<h3>Description</h3>
A Simple Todo API that lets you manage your daily Todo tasks.<br>

<h3>Tech Stack</h3>
It is written in .NET 8 and utilizes AWS DynamoDB for data persistence.<br>

<h3>APIs</h3>
  <ol>
    <li>
      <h4>GET all Todos</h4>
      <p>
        <i>Path</i>: /todo<br>
        <i>HTTP Method</i>: GET<br>
        <i>Request Body</i>: Not required<br>
      </p>
    </li>
    <li>
      <h4>GET one Todo</h4>
      <p>
        <i>Path</i>: /todo/{id}<br>
        <i>HTTP Method</i>: GET<br>
        <i>Request Body</i>: Not required<br>
      </p>
    </li>
    <li>
      <h4>Insert a Todo</h4>
      <p>
        <i>Path</i>: /todo<br>
        <i>HTTP Method</i>: POST<br>
        <i>Request Body</i>: { "task": {task-name}, "isComplete": {true/false} }
      </p>
    </li>
    <li>
      <h4>Update a Todo</h4>
      <p>
        <i>Path</i>: /todo<br>
        <i>HTTP Method</i>: PUT<br>
        <i>Request Body</i>: { "id": {id-of-the-task}, "task": {task-name}, "isComplete": {true/false} }
      </p>
    </li>
    <li>
      <h4>Delete a Todo</h4>
      <p>
        <i>Path</i>: /todo/{id}<br>
        <i>HTTP Method</i>: DELETE<br>
        <i>Request Body</i>: Not required
      </p>
    </li>
  </ol>
