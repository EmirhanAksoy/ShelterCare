# Shelter Care 🐈 🐕 🏡

The Shelter Care Application is a monolithic software solution designed to help organizations organize and manage animal shelters effectively. It simplifies the management of shelter-related tasks, making it easier to care for and support animals in need.

## Table of Contents

- [Introduction](#introduction)
- [Technology Stack](#technology-stack)
- [Getting Started](#getting-started)
- [Installation](#installation)

## Introduction

Animal shelters play a crucial role in the welfare of animals. The Shelter Care Application is built to streamline the management of shelter operations, making it easier for shelter staff and volunteers to provide care, record information, and maintain records for the animals in their care.

## Technology Stack

The Shelter Care Application is developed using the following technologies:

- **.NET 8**: The application is built on the latest version of the .NET framework, ensuring high performance and scalability.

- **Dapper**: Dapper is used as the data access library to interact with the PostgreSQL database. It provides a fast and efficient way to query and manipulate data.

- **Serilog**: Serilog is used for structured logging, making it easier to track and troubleshoot issues in the application.

- **Fluent Validation**: Fluent Validation is used for input validation and ensuring data integrity.

- **CQRS & Mediator**: The application follows the CQRS (Command Query Responsibility Segregation) pattern with the help of Mediator, separating command and query responsibilities for better maintainability and scalability.

- **PostgreSQL**: PostgreSQL is the chosen relational database management system, offering robust data storage and retrieval capabilities.

## Getting Started

To get started with the Shelter Care Application, follow these steps:

## Installation

1. **Clone the Repository**: Clone the Shelter Care Application repository to your local development environment.

   ```bash
   git clone https://github.com/EmirhanAksoy/ShelterCare.git
   ```

2. **Set Up**:
   
   - Install [Docker](https://www.docker.com/products/docker-desktop/) desktop
   - Open the terminal on ```..ShelterCare\src\ShelterCare.API\Docker```
   - Run ```docker-compose up```


We welcome contributions from the community! If you'd like to contribute to the Shelter Care Application, please follow our [contributing guidelines](CONTRIBUTING.md).

## License

The Shelter Care Application is open-source software licensed under the [MIT License](LICENSE.md). You are free to use, modify, and distribute it as per the terms of the license.
