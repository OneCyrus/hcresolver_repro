the graphql query. basically it should limit the pills to the color of "Neo" (`neo(color: RED)`) when specified. null should return every color.

```
{
  redNeo: neo(color: RED) {
    x
    colorFilter
    pills {
      id
      color
    }
  }

  blueNeo: neo(color: BLUE) {
    x
    colorFilter
    pills {
      id
      color
    }
  }

  test : neo {
    x
    colorFilter
    pills {
      id
      color
    }
  }
}
```

response which includes red and blue pills for every "neo". it's ignoring the filter.

```
{
  "data": {
    "redNeo": [
      {
        "x": 1,
        "colorFilter": "RED",
        "pills": {
          "id": 1,
          "color": "RED"
        }
      },
      {
        "x": 2,
        "colorFilter": "RED",
        "pills": {
          "id": 2,
          "color": "BLUE"
        }
      },
      {
        "x": 3,
        "colorFilter": "RED",
        "pills": {
          "id": 3,
          "color": "RED"
        }
      }
    ],
    "blueNeo": [
      {
        "x": 1,
        "colorFilter": "BLUE",
        "pills": {
          "id": 1,
          "color": "RED"
        }
      },
      {
        "x": 2,
        "colorFilter": "BLUE",
        "pills": {
          "id": 2,
          "color": "BLUE"
        }
      },
      {
        "x": 3,
        "colorFilter": "BLUE",
        "pills": {
          "id": 3,
          "color": "RED"
        }
      }
    ],
    "test": [
      {
        "x": 1,
        "colorFilter": null,
        "pills": {
          "id": 1,
          "color": "RED"
        }
      },
      {
        "x": 2,
        "colorFilter": null,
        "pills": {
          "id": 2,
          "color": "BLUE"
        }
      },
      {
        "x": 3,
        "colorFilter": null,
        "pills": {
          "id": 3,
          "color": "RED"
        }
      }
    ]
  }
}
```