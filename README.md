# Kokku-SE-Test

#Log of changes made in the project

-> I created the GameManager class to encapsulate the game behavior in a non-static class, making the responsibility of the Main function only to start the game. That way, I've improved readability and avoided the use of local functions or the use of static variables and methods. 

-> In the GameManager I've separated all its behavior into two main flows, the Setup of the game and the Game Loop. The Setup was responsible for starting the game and creating the characters and the battlefield, while the game loop was responsible for dealing with the logic of the match and the characters' turns.

-> I've removed all the recursive methods in the code, replacing them with a regular iteration.

->  In the Setup Flow:

	-> I've created the InitializationInputReader with the responsibility of reading the player's input, encapsulating this behavior and its methods in one class.  

	-> I created the Battlefield class to be an abstraction layer between the Grid and the rest of the systems.

	-> I created the IBattlefieldEntity interface so that the battlefield class generically treated its entities. With this, I make the battlefield system more scalable, since in the future it is possible to create new types of entities that can be present on the battlefield without being the characters.

	-> I centralized the character creation logic directly in the Character class constructor.

	-> I created the abstract class ACharacterClass, which holds the specific data of a character class like stats, skills, etc.
	-> I unified the specific functions of the enemy and player, treating them generically.


-> In the Game Loop Flow
	-> I remade the entire GameLoop flow and architecture.
	-> I made an algorithm to make the character's target dynamic, finding the nearest enemy at the beginning of its turn.

	-> Turns are now based on ICharacterActions, which define possible one-turn actions for the character, be they move actions, attack actions, or status actions. On their turns, each character decides the main action to be performed and has a chance to activate their passive action.

	-> damage is no longer random and now follows a formula depending on the status of each class.

	-> I made a status buff and debuff system;

	-> I made the endgame check logic, checking at the end of each turn if there is only one team left alive
