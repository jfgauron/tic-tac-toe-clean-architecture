# TicTacToe

## Run

In the devcontainer (or a similar environment with dotnet 8.0):

```
dotnet build
dotnet run --project src/TicTacToe/TicTacToe.csproj
```

The API should then be available on: http://localhost:8080

Here are curl payloads to target the different endpoints:

### Create a TicTacToe game

```
curl -X POST "http://localhost:8080/api/games" \
  -H "Content-Type: application/json" \
  -d '{"gridSize": 3}'
```

Response example:

```
{"id":"2d515cb7-e9a4-4983-ba72-78862fd2a043","grid":["","","","","","","","",""],"winner":""}
```

### Get a TicTacToe game

```
curl -X GET "http://localhost:8080/api/games/2d515cb7-e9a4-4983-ba72-78862fd2a043"
  -H "Accept: application/json"
```

### Make a move:

```
curl -X PATCH "http://localhost:8080/api/games/2d515cb7-e9a4-4983-ba72-78862fd2a043" \
  -H "Content-Type: application/json" \
  -d '{"x": 2, "y": 0}'
```