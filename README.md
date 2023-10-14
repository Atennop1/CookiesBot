# CookiesBot
![badge](https://img.shields.io/static/v1?label=Language&message=C%23&color=blueviolet&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=architecture&message=Pure-Model&color=red&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=database&message=postgresql&color=blue&style=for-the-badge)

## About project 
This is a project I did as practice in databases and fancy games. This is something like a clicker where you need to get cookies. There are regular and gold cookies, the first can be obtained simply by pressing a button, and the second can only be obtained once per hour. In the bot you need to turn on farm mode to start getting cookies. Then you can turn off this mode as unnecessary. This is also where I implemented my update loop and screen system so that after the bot reboots the player's state can be loaded. It was also planned to make a store, but I started other projects and as a result forgot about this one, and after that I didn’t want to finish it anymore, only the inventory with items was implemented. All data is stored in a database and searched by Telegram account ID. Below you can see how the bot works.

![image](https://github.com/Atennop1/CookiesBot/assets/73060890/92f33ac7-0da3-4ac9-9839-259acee32742)

## Specifics
- Project using **OOP** and **SOLID**
- Made with [**my database library**](https://github.com/Atennop1/Relational-Databases-Via-OOP)
- In this project I experimented with abstractions, it turned out cool
- The project was made for teaching databases in practice and for research OOP

## Tools
- [**Telegram.BotAPI**](https://github.com/Eptagone/Telegram.BotAPI)
- [**RelationalDatabasesViaOOP**](https://github.com/Atennop1/Relational-Databases-via-OOP)

## Conclusion
In conclusion, I would like to say that the project really added to my experience. It was interesting to design the game architecture in the conditions of a telegram bot. It was also interesting to work with databases, to think about how to implement this or that functionality with their help. Well, most of all it was pleasant to realize that all work with databases is carried out through my own library.
