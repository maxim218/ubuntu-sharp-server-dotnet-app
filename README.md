# Server C Sharp Language

### Creating application and run it

```
dotnet new webapp
dotnet restore
dotnet run
```

### Application programming interface

POST

```
curl --data '{"operation": "div", "first": 17, "second": 8}' --header "Content-Type: application/json" http://localhost:5288/api/calculate/post/operation
```

```
curl --data '{"operation": "mod", "first": 17, "second": 8}' --header "Content-Type: application/json" http://localhost:5288/api/calculate/post/operation
```

GET

```
curl "http://localhost:5288/api/calculate/summa?a=12&b=8"
```

```
curl "http://localhost:5288/api/calculate/multiply/json?a=5&b=300"
```

```
curl "http://localhost:5288/page/get?p=max"
```

```
curl "http://localhost:5288/page/get?p=geo"
```


