using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Todo.DAL.Constants;
using Todo.DAL.Repositories.Interfaces;
using Todo.Models;
using Todo.Models.DomainModels;
using Todo.Models.DTO;

namespace Todo.DAL.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private readonly ILogger<BaseRepository> _logger;

        public BaseRepository(IAmazonDynamoDB dynamoDBClient, ILogger<BaseRepository> logger)
        {
            _dynamoDBClient = dynamoDBClient;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<dynamic> DeleteAsync(Guid id)
        {
            try
            {
                var attributeMap = new Dictionary<string, AttributeValue>()
                {
                    { DBConstants.DB_TABLE_PRIMARY_KEY, new AttributeValue() { S = id.ToString() } },
                    { DBConstants.DB_TABLE_SORT_KEY, new AttributeValue() { S = id.ToString() } }
                };
                DeleteItemRequest deleteRequest = new DeleteItemRequest()
                {
                    TableName = DBConstants.DYNAMO_DB_TABLE_NAME,
                    Key = attributeMap
                };

                var response = await _dynamoDBClient.DeleteItemAsync(deleteRequest);
                return response.HttpStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during DELETE operation: {ex}");
                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TodoDTO?>> GetAllItemsAsync()
        {
            try
            {
                ScanRequest scanRequest = new ScanRequest()
                {
                    TableName = DBConstants.DYNAMO_DB_TABLE_NAME
                };

                var response = await _dynamoDBClient.ScanAsync(scanRequest);
                return response.Items.Select(item =>
                {
                    var json = Document.FromAttributeMap(item).ToJson();
                    return JsonSerializer.Deserialize<TodoDTO>(json);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during GET operation: {ex}");
                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<TodoDTO?> GetItemAsync(Guid id)
        {
            try
            {
                var attributeMap = new Dictionary<string, AttributeValue>()
                {
                    { DBConstants.DB_TABLE_PRIMARY_KEY, new AttributeValue() { S = id.ToString() } },
                    { DBConstants.DB_TABLE_SORT_KEY, new AttributeValue() { S = id.ToString() } }
                };
                GetItemRequest getItemRequest = new GetItemRequest()
                {
                    TableName = DBConstants.DYNAMO_DB_TABLE_NAME,
                    Key = attributeMap
                };

                var response = await _dynamoDBClient.GetItemAsync(getItemRequest);
                if (response.Item.Count == 0)
                    return null;

                var itemDocument = Document.FromAttributeMap(response.Item);
                return JsonSerializer.Deserialize<TodoDTO>(itemDocument.ToJson());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during GET operation: {ex}");
                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<dynamic> InsertAsync(TodoModel todo)
        {
            try
            {
                DynamoDBObject dbObject = CreateDynamoDBObjectFromDomainModel(todo);
                var jsonObject = JsonSerializer.Serialize(dbObject);
                var attributeMap = Document.FromJson(jsonObject).ToAttributeMap();
                PutItemRequest insertRequest = new PutItemRequest()
                {
                    TableName = DBConstants.DYNAMO_DB_TABLE_NAME,
                    Item = attributeMap
                };

                var response = await _dynamoDBClient.PutItemAsync(insertRequest);
                return response.HttpStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during INSERT operation: {ex}");
                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<dynamic> UpdateAsync(TodoModel todo)
        {
            try
            {
                DynamoDBObject dbObject = CreateDynamoDBObjectFromDomainModel(todo);
                var jsonObject = JsonSerializer.Serialize(dbObject);
                var attributeMap = Document.FromJson(jsonObject).ToAttributeMap();
                PutItemRequest updateRequest = new PutItemRequest()
                {
                    TableName = DBConstants.DYNAMO_DB_TABLE_NAME,
                    Item = attributeMap
                };

                var response = await _dynamoDBClient.PutItemAsync(updateRequest);
                return response.HttpStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during UPDATE operation: {ex}");
                throw ex;
            }
        }

        #region Private Methods

        private DynamoDBObject CreateDynamoDBObjectFromDomainModel(TodoModel todo)
        {
            return new DynamoDBObject()
            {
                Id = todo.Id == null ? Guid.NewGuid() : (Guid)todo.Id,
                Task = todo.Task,
                IsComplete = todo.IsComplete,
            };
        }

        #endregion
    }
}
