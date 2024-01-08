# AICook - AI Powered Recipe Generation

AICook is a project that combines the power of OpenAI's ChatGPT-4 Turbo and DALL-E 2 to create recipes. The backend is built using ASP.NET and uses a microservices architecture to create a scalable and efficient system.

## Architectural Overview

AICook is composed of four main projects:

1. **`AICook.AIWorkerService`**: This service interacts with OpenAI to generate recipes and communicate with DALL-E 2 to create corresponding images. It uploads the generated images to a Backblaze B2 Bucket for persistence.

2. **`AICook.Event`**: This project contains the contracts used for event bus communication.

3. **`AICook.Gateway`**: Using YARP (Yet Another Reverse Proxy), this gateway manages and routes incoming requests.

4. **`AICook.RecipeService`**: This service exposes a REST API for recipe management. This service also stores recipe data obtained by `AICook.AIWorkerService`.

Services can communicate with each other using the MassTransit library and the RabbitMQ message broker.

All services are orchestrated using Docker containers, allowing for easy setup and deployment.

## Getting Started

### Prerequisites

- Docker and Docker-Compose
- OpenAI API key
- Backblaze B2 bucket
- .NET 8.0 SDK (if running outside of Docker)
- RabbitMQ Server (if running outside of Docker)

### Environment Setup

1. Clone the repository to your local system.
2. Ensure Docker is running on your machine.
3. Update the environment variables in `AICook.RecipeService` and `AICook.AIWorkerService` project folders with your specific configurations.

### Running the Application

```bash
docker-compose up --build
```

This command will set up all the required services, including the gateway, in a series of connected Docker containers.

### Endpoint Usage

The services can be accessed through the following endpoints:

- **Create a new recipe**:
  - `POST /api/recipe/create`
  - Include a JSON body with a `prompt` property containing your recipe idea.

```json
{
  "prompt": "Vegan chocolate cake"
}
```

- **List all recipes**:
  - `GET /api/recipe`
  - Fetches a list of all stored recipes.

- **Get a single Recipe**:
  - `GET /api/recipe/{id}`
  - Gets a single recipe by its unique identifier.

By deafult the gateway is hosted on port 5000. Ensure to use this port when sending requests to the above endpoints.

For example:

```
http://localhost:5000/api/recipe/create
http://localhost:5000/api/recipe
http://localhost:5000/api/recipe/{id}
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
