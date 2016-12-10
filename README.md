# Hair Salon

#### December 9, 2016

### By **Steve Burton**

### Specifications
This project will create an app for a hair salon with a database to hold stylist and client deatils and show which clients belong to each stylist.

These are the steps I'll take to write my code:

One I'll test to ensure the database is empty to begin with.
* Input: 0
* Output: 0

Two I'll check for equality if the names are the same.
* Input: "Jenny"
* Output: "Jenny"

Three, test to ensure the user can save to the database.
* Input: "Jenny"
* Output: "Jenny"

Four, test to ensure an ID is assigned to a stylist.
* Input: 1
* Output: 1

Five, test that the user can find a stylist's details in the database by ID.
* Input: Stylist ID
* Output: "Jenny", "Portland"

Six, test that the user can find a client's details in the database by ID.
* Input: Client ID
* Output: "Susan", "Portland"

Seven, test that the user can find all the clients who belong to a stylist.
* Input: Client name
* Output: Stylist name

Eight, test to update stylist details.
* Input: "Jenny", "Portland"
* Output: "Jenny", "Oregon City"

Nine, test to update client details.
* Input: "Susan", "Portland", "Jenny"
* Output: "Susan", "Gresham", "Jessica"

Ten, test to delete stylist
* Input: "Jenny"
* Output: "Empty"

Eleven test to delete client
* Input: "Susan"
* Output: "Empty"


## Setup/Installation Requirements

Set up a database:
* CREATE DATABASE hair_salon;
* GO
* USE hair_salon;
* GO
* CREATE TABLE stylists (id INT IDENTITY(1,1), stylist_name VARCHAR(255), stylist_details VARCHAR(255));
* CREATE TABLE clients (id INT IDENTITY(1,1), client_name VARCHAR(255), client_details VARCHAR(255), stylist_id INT);
* GO

To run the application:
* Clone this repository or download it to your computer.
* Navigate to the project directory in the terminal.
* Use the command > dnu restore to download any necessary dependencies.
* Use the command > dnx kestrel to run the project on the local server.
* Navigate to localhost:5004 in your browser to view the app

## Known Bugs

None.

## Support and contact details

You can contact me on Github at steve-burton.

## Technologies Used

HTML, CSS, Bootstrap, C#, Sql

### License

GPL

Copyright (c) 2016 **_Steve Burton_**
