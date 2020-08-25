# MyFirstBlog

This blog is based on the technical task:

Create a simple web site for users to create/submit articles and post comments. Add user management and authentication. 
Start from back-end implementation.
	
## Back-end

### Complete application must meet the following criterias:
- Application was written with .NET CORE 3.x by using REST architecture (use HTTP and JSON) as a microservice;
- Authentication. Only authenticated users can post articles and comments;
- Any user can view content. User information should consist of UserName, Email and Password. Not allowed to create two users with identical name or email;    --
- Article length can't be more than 2000 symbols. Article required fields: Title, CreatedUser, CreatedDate, *Category;
- Comment length is limited to 200 symbols; 
- User can't create a post with already existing article title;


## Unit Testing  - Missing


## UI - Swagger

## DB
- Used MongoDB to store all necessary information;
- Provide CRUD operations for each document.

