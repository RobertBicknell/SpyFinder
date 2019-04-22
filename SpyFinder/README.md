SpyFinder

Author:		
	Robert Bicknell

Date:		
	April 20th 2019

Description:
	SpyFinder is a minimal web application to manage a list of spy names and codes, and to check messages for the presence of a spy's code.  
	SpyFinder stores spy names and codes in a PostgresSQL database as defined in the configuration setting  
		PROD: "Host=localhost;Database=Spies;Username=postgres;Password=password"
		TEST: "Host=localhost;Database=Spies;Username=postgres;Password=password"

HTTP GET
	Purpose: Returns list of Spies and their codes.
	Example request URL: GET http://localhost/api/spies
	Example request payload : none.
	Example response payload : [{"name":"Ethan Hunt","code":[3,1,4]},{"name":"James Bond","code":[0,0,7]}]

HTTP PUT
	Purpose: Saves a new Spy to the database.
	Example request URL: PUT http://localhost/api/spies
	Example request payload : 
		{
			"name" : "James Bond",
			"code" : [0,0,7]
		}
	Example response payload : ***ADD***

HTTP DELETE
	Purpose: Deletes a Spy from the database
	Example request URL: DELETE http://localhost/api/spies/<spy name>
	Example request payload : none.
	Example response payload : ***ADD***

HTTP POST
	Purpose: Checks a message for a spy's code and return whether the code is present.
	Example request URL: POST http://localhost/api/spies/
	Example request payload : 
		{
			"Message" : [1,0,1,0,7,1],
			"Spy" : "James Bond"
		}	
	Example response payload : ***ADD***

Preparation 
	* Install Postgres
	* Create two copies of teh SPies datbase, for PROD and TEST using the SQL script contained in the SpyFinder Data project: 
		CreateDatabase.Spies.Postgres.sql
	* Modify the Configuration settings for the PostgresSQL PROD and TEST connections strings as required.
		Example: "Host=localhost;Database=Spies;Username=postgres;Password=password"

Docker:
	***ADD

Developer Testing notes for QA:
	* Testing was performed using Postman
	** Configure the correct HTTP Verb, URL, including any custom port
	** Add a Content-type header with MIME-Type 'application/json'
	** Add a request payload if required as shown below for each request.
	** Verify the returned result by comparing to the sample outputs shown for each request.

	Not suported:
		* authorization
		* https
		* cors
		* csrf tokens
		* caching
		* logging 
		* custom exception handling

Technology:
	.net Core 2.2.0 
	Npgsql Npgsql.EntityFrameworkCore.PostgreSQL 2.2.0
	PostgresSQL 11
	
Notes 
	The method of checking a message for a code compares elements of integer arrays.
	Examples:

		Message			Code				Result
		-------------------------------------------------------
		[1, 0]			[1, 0]				Spy is present 
		[1, 0]			[0, 1]				Spy is NOT present 
		[1, 0, 2]		[1, 2]				Spy is present 
		[1, 0]			[10]				Spy is NOT present 
