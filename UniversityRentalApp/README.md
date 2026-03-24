# University Equipment Rental System

## How to Run
1. Open the `UniversityRentalApp.sln` file in Visual Studio.
2. Set `UniversityRentalApp` as the startup project.
3. Click the green "Start" (Play) button at the top menu, or press F5.
4. The console window will open and automatically run the demonstration scenario, showing successful rentals, blocked limit attempts, and penalty calculations.

## Design Decisions and Architecture
Instead of putting all the logic into `Program.cs` or one massive `App` class, I separated the project into distinct folders (Models, Repositories, Rules, and Services) to ensure high cohesion and loose coupling.

### 1. Single Responsibility Principle (SRP) & Cohesion
Every class in this project has one clear job. The `Models` only hold data. The `Repositories` only handle storing and finding data. The `RentalService` handles the actual business logic of renting and returning. 

### 2. Loose Coupling & Dependency Inversion
I passed the repositories and rules into the `RentalService` constructor. This follows the Dependency Inversion Principle from our slides, keeping the classes loosely coupled. 

### 3. Inheritance and Polymorphism
I created an abstract base class called `Equipment`. You cannot instantiate generic equipment; it must be a specific type (`Laptop`, `Projector`, or `Camera`). I used the `virtual` keyword on the `GetDescription()` method in the base class, and used `override` in the subclasses to add their specific details (like RAM for laptops, or Lumens for projectors). 

### 4. Business Rules via Interfaces
To make the penalty rules and user limits easy to modify without breaking existing code, I created the `IRentalLimitRule` and `IPenaltyRule` interfaces. We can simply create a new rule class that implements the interface.