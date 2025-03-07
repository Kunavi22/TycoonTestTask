# Tycoon Test Task
Theme: Civilization Building
## Mechanics List

### UI/UX

- Main menu
- Basic UI
- UI crafting system
- Tooltip system

### Building/Explore Element
This element allows players to construct and upgrade buildings or unlock new territories.
- Upgrade costs, construction time, and events triggered by construction can be configured in the Unity Inspector.

![image](https://github.com/user-attachments/assets/560008af-c9ae-4ad2-87f3-ffac039b8f87)

### Basic Building/Creation System

Players can build and upgrade structures in designated locations. Created buildings can be of three types:
- Generation Buildings - produce resources.
- Crafting Buildings - unlock crafting recipes.
- Idle Buildings - have no functionality but are used in combination with building element events to progress the game.

![image](https://github.com/user-attachments/assets/eba0ec2a-08ea-4b28-8762-61873d8721cf)

### Basic Inventory and Crafting System

A player inventory is implemented to store resource amounts.

![image](https://github.com/user-attachments/assets/78adc6a5-f58f-4db4-92b2-1f29b022bfc6)

- The crafting system manages resource transformations.
- Crafting buildings includes they recipes to the system after construction.

![image](https://github.com/user-attachments/assets/280a8069-2ee7-4a61-a4cf-7e98500011b1)

### Basic Progression System
- Players can unlock terrain sections through the building element.
- By adding events to the building element, other mechanics can be hidden/revealed to enable game progression.

### Custom JSON-Based Save System (Auto Save)

- The save system stores the state of resources and constructed buildings.
- IDs are automatically assigned to building elements via the OnValidate() function, ensuring proper save data identification.
- Save files remain valid even after updating the application, as long as existing building IDs are not modified.

Save triggers:
- Every minute
- When constructing a building
- When returning to the main menu

### Map Design
A small demonstration map was created to showcase all implemented mechanics.

![image](https://github.com/user-attachments/assets/f3059da6-fb09-4643-953f-83e66e0a005d)

## Additional Info
During development, I focused on completing the tasks without extra mechanics to avoid wasting time on unexpected redesigns. I aimed to complete all assignments as efficiently as possible while maintaining clean code and functional mechanics.

Overall, the project took me 2 days to complete.
