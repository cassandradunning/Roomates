# Roomates
### Writing SQL in a .NET Application with ADO.NET

## Create a Chore Repository

1. Create a new file in the Repositories folder called `ChoreRepository` and implement the same methods as we did with the `RoomRepository`. After implementing each method, update the `Main` method to add an option in the menu.

1. Create a `RoommateRepository` and implement only the `GetById` method. It should take in a `int id` as a parameter and return a `Roommate` object. The trick: When you add a menu option for searching for a roommate by their Id, the output to the screen should output their first name, their rent portion, and _the name of the room they occupy_. Hint: You'll want to use a JOIN statement in your SQL query

1. Add a method to `ChoreRepository` called `GetUnassignedChores`. It should not accept any parameters and should return a list of chores that don't have any roommates already assigned to them. After implementing this method, add an option to the menu so the user can see the list of unassigned chores.

1. Add a `RoommateRepository` and define a `GetAll` method on it, but don't add a menu option to view all roommates yet. Next create a method in the `ChoreRepository` named `AssignChore`. It should accept 2 parameters--a roommateId and a choreId. Finally, add an option to the menu for "Assign chore to roommate". When the user selects that option, the program should first show a list of all chores and prompt the user to select the Id of the chore they want. Next the program should show a list of all roommates and prompt the user to select the Id of the roommate they want assigned to that chore. After the roommate has been assigned to the chore the program should print a message to the user to let them know the operation was successful.

