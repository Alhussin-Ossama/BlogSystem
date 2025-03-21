### 📌 Blog System API  

A **role-based** blogging platform built with **ASP.NET Core 8**, enabling **secure post management, commenting, and user authentication** with advanced search and filtering capabilities.  

---

## 🚀 Features  

- 🔑 **JWT Authentication & Role-Based Access Control (RBAC)**  
- 📝 **Create, Edit, and Delete Blog Posts** (Admin, Editor)  
- 💬 **Commenting & Replies** (Reader, Editor, Admin)  
- 🔍 **Search by Title, Category, or Tags**  
- 🎯 **Filter Posts by Status (Draft, Published, Archived)**  
- 🛠️ **Specification Pattern & Middleware for Performance & Error Handling**  

---

## 🛠️ Tech Stack  

- **Backend**: ASP.NET Core 8 Web API, Entity Framework Core  
- **Database**: SQL Server  
- **Authentication**: JWT Tokens, ASP.NET Identity  
- **API Documentation**: Swagger / Postman  

---

## 📜 API Endpoints  

### 🔑 Authentication  
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/api/account/register` | Register a new user |
| `POST` | `/api/account/login` | Authenticate and get a JWT token |
| `GET`  | `/api/account/getcurrentuser` | Retrieve logged-in user details |

### 📝 Blog Posts  
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/api/posts` | Get all posts |
| `GET`  | `/api/posts/{id}` | Get post by ID |
| `POST` | `/api/posts` | Create a new post (Admin, Editor) |
| `PUT`  | `/api/posts/{id}` | Update an existing post (Admin, Editor) |
| `DELETE` | `/api/posts/{id}` | Delete a post (Admin) |

### 💬 Comments & Replies  
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/api/comments/{postId}` | Get comments for a post |
| `POST` | `/api/comments` | Add a comment to a post (Reader, Editor, Admin) |
| `POST` | `/api/comments/reply` | Add a reply to a comment (Reader, Editor, Admin) |
| `PUT`  | `/api/comments/{id}` | Edit a comment (Owner, Admin) |
| `DELETE` | `/api/comments/{id}` | Delete a comment (Admin) |

### 🔎 Search & Filtering  
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/api/posts/search?title=...&category=...&tag=...` | Search posts by title, category, or tags |
| `GET`  | `/api/posts/filter?status=draft` | Filter posts by status (Draft, Published, Archived) |

---

## ⚙️ Installation & Setup  

### 1️⃣ Clone the Repository  
```sh
git clone https://github.com/Alhussin-Ossama/BlogSystem.git
cd BlogSystem/BlogSystem.API
```

### 2️⃣ Configure the Database  
Update `appsettings.json` with your **SQL Server connection string**.

### 3️⃣ Apply Migrations & Seed Data  
```sh
dotnet ef database update
```

### 4️⃣ Run the API  
```sh
dotnet run
```

### 5️⃣ Access API Documentation  
Open your browser and go to:  
🔗 [`http://localhost:port/swagger`](http://localhost:port/swagger)  

---

## 🤝 Contributing  
Contributions are welcome! Fork the repository, raise issues, or submit pull requests. 🚀  

## 📧 Contact  
📩 **Email**: [hussinossama44@gmail.com](mailto:hussinossama44@gmail.com)  
🔗 **GitHub**: [Alhussin-Ossama](https://github.com/Alhussin-Ossama/BlogSystem)  
