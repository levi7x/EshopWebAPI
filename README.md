# MusicEshop - currently working on this project!

## Backend API of the app
Link to front end source code, please click <a href="https://github.com/levi7x/eshop-react"> HERE </a> to see details.

<h2> About </h3>
<p>
This is eCommerce website for music equipment. Generaly this could be used for any type of shop because of its modularity. This project simulates everything you can expect from online shop from displaying catalog, browsing through it and ultimately making your own order.
</p>

<h2> What i used for backend </h2>
<p>
This project was created with ASP .NET CORE WEB API and SQL Database for backend. The core is the Entity Framework for connecting database with the API, and Identity framework for making work with different user roles easier. I used agile approach for building this website. The code is structured following repository design pattern. I also implemented custom logger for displaying warnings and info. For unit testing i used tool FakeItEasy. For  Here and there i used the new ChatGPT bot for help with making my code more clean.
</p>

<h2> Goal </h2>
<p>
The goal of this project is to create website that solves business problem. Mainly to improve myself in coding with ASP NET + React.js and applying different concepts i learned over time i ve been writing code without following any tutorials. Ultimate goal is to deploy finished project on Mircrosoft Azure.
</p>

<h2> Use cases </h2>
<p>
Already done:
authentication, persist-login with jwt tokens, authorization, registration, role based access control, catalog, cart CRUD for users, user account management


Yet to do (currently working on): Admin dashboard- managing users, products , orders, categories + adding more admin roles (Superadmin, moderator), implementing search bar and category filter, encrypting credit card data, making better design for navbar, dropdown menu...

But still comming up with more and more ideas: adding rating and comments to products from users, adding warehouse for managing stock and much more...
</p>



<h2> Images from project </h2>

<h4> Current database model </h4>
<p> Still making changes as i add more features but for now im attaching this to grasp the concept</p>

![model](https://github.com/levi7x/MyImages/blob/main/ghub-imgs/eshop/model.png?raw=true)

<h4> Auth </h4>
<p> Login is made with JWT Bearer token and Refresh Token stored in HTTP only cookie that stores user information. The login persists even after refreshing page if the refresh token is valid. Register creates a brand new user </p> 

![log](https://github.com/levi7x/MyImages/blob/main/ghub-imgs/eshop/login.png?raw=true)

![reg](https://github.com/levi7x/MyImages/blob/main/ghub-imgs/eshop/reg.png?raw=true)

<h4> Cart </h4>
<p> User can add products from catalog to cart which are stored in the database. Each user has its own cart. The addition and substraction are dynamic thanks to react, so no form submition is needed</p>

![cart](https://github.com/levi7x/MyImages/blob/main/ghub-imgs/eshop/cart.png?raw=true)

<h4> Account </h4>
<p> Account displays the user information with many more features </p>

![acc1](https://github.com/levi7x/MyImages/blob/main/ghub-imgs/eshop/acc1.png?raw=true)
![acc2](https://github.com/levi7x/MyImages/blob/main/ghub-imgs/eshop/acc2.png?raw=true)
