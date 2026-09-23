# Live Sports – Darts

A real-time darts match reporting application built with **Blazor Server and .NET 8**.

This individual project was developed as part of an exam assignment focused on building a live sports application. I chose **darts** as the sport and created an application that allows matches and players to be managed, while also providing a live match reporting experience for reporters and fans.

## Project Status

This project is completed and was developed as an individual exam project.

The application includes match setup, player and country management, live darts scoring, match events, statistics and a live fan view.

## My Contribution

As this was an individual project, I developed the application independently from start to finish.

My work included:

* Designing and implementing the darts match logic
* Creating player and country management with CRUD functionality
* Building the database structure using Entity Framework Core
* Implementing seed data and database migrations
* Developing the match setup functionality
* Building the reporter interface for live match reporting
* Creating the live fan overview
* Implementing darts scoring, legs, sets and match progression
* Adding match events and highlights
* Calculating and displaying player statistics and averages
* Designing the application layout and user interface
* Using Blazor Server's built-in real-time communication for live UI updates

## Technologies

* **C#**
* **.NET 8**
* **Blazor Server**
* **Entity Framework Core**
* **SQL Server / SQL Server Express**
* **SignalR** through the built-in Blazor Server infrastructure
* **Razor Components**
* **Bootstrap**

## Key Functionality

### Player & Country Management

The application provides CRUD functionality for players and countries.

Players are linked to their country, allowing the application to display and manage player information in a structured way.

### Match Setup

Before a match starts, the reporter can select the players and configure the match.

The application then initializes the match state and prepares the live scoring environment.

### Live Darts Reporting

The reporter can record the progress of a darts match while it is being played.

The application keeps track of:

* Player scores
* Legs
* Sets
* Match progression
* Checkout situations
* Match winner
* Match events
* Player averages
* Highlights

The darts game logic is handled through a dedicated `DartGameService`.

### Live Fan Overview

The fan page provides a live view of the current match.

Instead of implementing a separate custom SignalR Hub, the application makes use of the **built-in real-time communication provided by Blazor Server**. Changes in the game state are propagated to the active components through the `DartGameService`, allowing the fan view to update while the match is being reported.

## Project Structure

The solution is divided into two main projects:

### `Pin.LiveSports.Core`

Contains the shared application logic and domain structure:

* Models
* Interfaces
* Enums

### `Pin.LiveSports.Blazor`

Contains the Blazor Server application:

* Pages and Razor components
* Services
* Database context
* Seed data
* Entity Framework migrations
* Application configuration

This separation keeps the core models and interfaces independent from the user interface.

## Project Context

This project was created as an individual exam assignment for a **Live Sports** application.

The assignment required a sports management application with CRUD functionality and a live reporting component. I chose darts because its scoring system provided an interesting challenge for implementing live match logic, including scores, legs, sets and checkout situations.

During development, I experimented with different approaches to real-time communication. I ultimately chose to work with the real-time capabilities already provided by Blazor Server instead of creating a separate custom SignalR Hub. This resulted in a simpler solution while still providing the required live interaction between the reporter and fan views.

## Running the Project

The project uses **SQL Server Express** with Windows Authentication.

The connection string is configured in `appsettings.json` and uses a local SQL Server Express database.

To run the project:

1. Make sure SQL Server Express is installed and running.
2. Open `Pin.LiveSports.sln` in **Visual Studio**.
3. Restore the NuGet packages.
4. Build the solution.
5. Run the `Pin.LiveSports.Blazor` project.

The application creates and seeds the database using Entity Framework Core migrations when the application starts.
