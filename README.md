# CensoEngine

Sample application which maps any kind of XML or Json to a specific output.
Mappings are added at runtime using xPath or JsonPath.

# Pre requisites
Docker (Linux containers)

# Running the application
1. Open the repository root folder on CMD
2. Run docker-compose up

Another alternative is to open the solution (.sln) with Visual Studio 2017, select docker-compose as the starting project and hit F5

# Authenticating
The application uses Jwt for authentication.
To get a token:
- POST: /api/login
- Headers: Content-Type: application/json
- Body:
```json
	{"username": "test", "password": "pass"}
```
Note: username test with password pass will be preconfigured in development environment

The returned token will be used as a Bearer Token for subsequent requests.

From now on, every example assumes the user has a valid token in every request header:
Ex:
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0IiwiZXhwIjoxNTI2NTI2NzEwfQ.p59A9Mp-VckoXHiAsNOfY1h8QrUXfTH3Y-q7SDfSJok

# Adding a new transformation
- POST: /api/CensoMapping
- Headers: Content-Type: application/json
- Body:
```json
{
    "name": "Transformation name (decoration only)",
    "cidadesPath": "JsonPath or xPath to a list of cities",
    "nomeCidadePath": "JsonPath or xPath to a city name, relative to a city object",
    "habitantesCidadePath": "JsonPath or xPath to a city population, relative to a city object",
    "bairrosPath": "JsonPath or xPath to a list of city neighborhoods, relative to a city object",
    "nomeBairroPath": "JsonPath or xPath to a neighborhood name, relative to a neighborhood object",
    "habitantesBairroPath": "JsonPath or xPath to a neighborhood population, relative to a neighborhood object"
}
```

You can find some examples at the end of this file.

# Transforing some input
- POST: /api/censo
- Headers: Content-Type: text/plain
- Body: *Example only*
```xml
<corpo>
    <cidade>
        <nome>Rio de Janeiro</nome>
        <populacao>10345678</populacao>
        <bairros>
            <bairro>
                <nome>Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao>135678</populacao>
            </bairro>
            <bairro>
                <nome>Botafogo</nome>
                <regiao>Zona Sul</regiao>
                <populacao>105711</populacao>
            </bairro>
        </bairros>
    </cidade>
</corpo>
```
Output:
```json
{
    "result": [
        {
            "cidade": "Rio de Janeiro",
            "habitantes": 10345678,
            "bairros": [
                {
                    "nome": "Tijuca",
                    "habitantes": 135678
                },
                {
                    "nome": "Botafogo",
                    "habitantes": 105711
                }
            ]
        }
    ]
}
```



# Transformation example for XML
A mapping example for the following input:
```xml
<corpo>
    <cidade>
        <nome>Rio de Janeiro</nome>
        <populacao>10345678</populacao>
        <bairros>
            <bairro>
                <nome>Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao>135678</populacao>
            </bairro>
            <bairro>
                <nome>Botafogo</nome>
                <regiao>Zona Sul</regiao>
                <populacao>12456</populacao>
            </bairro>
        </bairros>
    </cidade>
    <cidade>
        <nome>Petrópolis</nome>
        <populacao>300000</populacao>
        <bairros>
            <bairro>
                <nome>Mosela</nome>
                <regiao>Central</regiao>
                <populacao>21234</populacao>
            </bairro>
            <bairro>
                <nome>Retiro</nome>
                <regiao>Central</regiao>
                <populacao>51368</populacao>
            </bairro>
        </bairros>
    </cidade>
</corpo>
```

Would be:
```json
{
    "name": "RJ",
    "cidadesPath": "/corpo/cidade",
    "nomeCidadePath": "nome",
    "habitantesCidadePath": "populacao",
    "bairrosPath": "bairros/bairro",
    "nomeBairroPath": "nome",
    "habitantesBairroPath": "populacao"
}
```

Notice that the only xPath starting with the root element (/) is cidades. That's because all other elements will be relative to another element, and not from root.

Explanation:
```
"cidadesPath": "/corpo/cidade",
```
xPath to a list of *cidade*. Will return a list of cidade objects
```xml
Element='<cidade>
        <nome>Rio de Janeiro</nome>
        ...
    </cidade>'
Element='<cidade>
        <nome>Petrópolis</nome>
        ...
    </cidade>'
```

xPaths from the *cidade* element to *nome* and *populacao*.
```
    "nomeCidadePath": "nome",
    "habitantesCidadePath": "populacao",
```
Returns:
```xml
Element='<nome>Rio de Janeiro</nome>'
Element='<populacao>10345678</populacao>'
```
for Rio de Janeiro

Starting from the city element, xPath for a list of nieghborhoods
```
"bairrosPath": "bairros/bairro",
```
```xml
Element='<bairro>
                <nome>Tijuca</nome>
                <regiao>Zona Norte</regiao>
                <populacao>135678</populacao>
            </bairro>'
Element='<bairro>
                <nome>Botafogo</nome>
                <regiao>Zona Sul</regiao>
                <populacao>12456</populacao>
            </bairro>'
```
Finally, an xPath from a *bairro* element to their *nome* and *populacao*:
```
    "nomeBairroPath": "nome",
    "habitantesBairroPath": "populacao"
```

returns, for Botafogo
    
```xml
Element='<nome>Botafogo</nome>'
Element='<populacao>12456</populacao>'
```

# Transformation example for Json using JsonPath

A mapping example for the following input:
```json
{
    "regions":[
        {
            "name": "Triangulo Mineiro",
            "cities": [
                {
                    "name": "Uberlandia",
                    "population": 700001,
                    "neighborhoods": [
                        {
                            "name": "Santa Monica",
                            "zone": "Zona Leste",
                            "population": 13012
                        }
                    ]
                }
            ]
        },
        {
            "name": "Zona da Mata Mineira",
            "cities": [
                {
                    "name": "Juiz de Fora",
                    "population": 600000,
                    "neighborhoods": [
                        {
                            "name": "Costa Carvalho",
                            "zone": "Região Central",
                            "population": 80654
                        }
                    ]
                }
            ]
        }
    ]
}
```

Would be:
```json
{
    "name": "doubleRegionJson",
    "cidadesPath": "$.regions[*].cities[*]",
    "nomeCidadePath": "name",
    "habitantesCidadePath": "population",
    "bairrosPath": "neighborhoods[*]",
    "nomeBairroPath": "name",
    "habitantesBairroPath": "population"
}
```

Notice that the only JsoNPath starting with the root element (&) is cidades. That's because all other elements will be relative to another element, and not from root.

Explanation:
```
"cidadesPath": "$.regions[*].cities[*]",
```
xPath to a list of *cidade* inside a list of region. Will return a list of cidade objects from every region object.
```json
[
  {
    "name": "Uberlandia",
    ...
  },
  {
    "name": "Juiz de Fora",
    ...
  }
]
```

JsonPath from the *cidade* element to *nome* and *populacao*.
```
    "nomeCidadePath": "name",
    "habitantesCidadePath": "population",
```
Returns:
```json
["Uberlandia"]
[700001]
```
for Uberlandia

Starting from the city element, JsonPath for a list of neighborhoods
```
"bairrosPath": "neighborhoods[*]",
```
```json
[
  {
    "name": "Santa Monica",
    "zone": "Zona Leste",
    "population": 13012
  }
]
```
Finally, a JsonPath from a *bairro* element to their *nome* and *populacao*:
```
    "nomeBairroPath": "name",
    "habitantesBairroPath": "population"
```

returns, for Santa Monica
    
```xml
["Santa Monica"]
[13012]
```

# Default configuration
By default, the application is configured with the following mappings:
```json
[
    {
        "name": "AC",
        "cidadesPath": "$.cities[*]",
        "nomeCidadePath": "name",
        "habitantesCidadePath": "population",
        "bairrosPath": "neighborhoods[*]",
        "nomeBairroPath": "name",
        "habitantesBairroPath": "population"
    },
    {
        "name": "MG",
        "cidadesPath": "/body/region/cities/city",
        "nomeCidadePath": "name",
        "habitantesCidadePath": "population",
        "bairrosPath": "neighborhoods/neighborhood",
        "nomeBairroPath": "name",
        "habitantesBairroPath": "population"
    },
    {
        "name": "RJ",
        "cidadesPath": "/corpo/cidade",
        "nomeCidadePath": "nome",
        "habitantesCidadePath": "populacao",
        "bairrosPath": "bairros/bairro",
        "nomeBairroPath": "nome",
        "habitantesBairroPath": "populacao"
    }
]
```

# Other funcionalities
- GET: /api/CensoMapping
    - Returns all Mappings currently registered
- GET: /api/CensoMapping/*name*
   - Returns a map by its name
- DELETE: /api/CensoMapping/*name*
    - Deletes a mapping by its name
- PUT: /api/CensoMapping/*name*    with CensoMapping body 
    - Updates a mapping by its name