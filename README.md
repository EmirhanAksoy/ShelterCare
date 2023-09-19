# Shelter Care 🐈 🐕 🏡

The Shelter Care Application is a monolithic software solution designed to help organizations organize and manage animal shelters effectively. It simplifies the management of shelter-related tasks, making it easier to care for and support animals in need.

## Table of Contents

- [Introduction](#introduction)
- [Technology Stack](#technology-stack)
- [Features](#features)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

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

## Features

The Shelter Care Application provides the following features:

- **Animal Management**: Record and manage information about animals in the shelter, including their species, breed, age, medical history, and adoption status.

- **Employee Management**: Manage employee details, including their roles, contact information, and employment history.

- **Area Management**: Organize the shelter into different areas, track their capacity, and manage the animals housed in each area.

- **Logging**: Utilize Serilog for comprehensive logging, helping administrators troubleshoot issues and maintain system health.

- **Validation**: Implement Fluent Validation to ensure that data entered into the system is valid and consistent.

- **CQRS & Mediator**: Employ the CQRS pattern to separate command and query responsibilities, enhancing the maintainability and scalability of the application.

- **Database Integration**: Utilize PostgreSQL for robust data storage and retrieval, ensuring data consistency and reliability.

## Getting Started

To get started with the Shelter Care Application, follow these steps:

## Installation

1. **Clone the Repository**: Clone the Shelter Care Application repository to your local development environment.

   ```bash
   git clone https://github.com/EmirhanAksoy/ShelterCare.git
   ```

2. **Set Up PostgreSQL**: Install and set up PostgreSQL as the database system for the application. Ensure you have the necessary connection string information.

3. **Configure App Settings**: Modify the application's configuration settings, including the database connection string and other environment-specific variables.

4. **Build and Run**: Build and run the application using .NET 7.

   ```bash
   dotnet build
   dotnet run
   ```

## Usage

Once the application is up and running, you can access it via a web interface or API endpoints. Refer to the application's documentation for detailed instructions on how to use each feature.

## Contributing

We welcome contributions from the community! If you'd like to contribute to the Shelter Care Application, please follow our [contributing guidelines](CONTRIBUTING.md).

## License

The Shelter Care Application is open-source software licensed under the [MIT License](LICENSE.md). You are free to use, modify, and distribute it as per the terms of the license.
