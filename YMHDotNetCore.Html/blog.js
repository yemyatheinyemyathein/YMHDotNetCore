const tblBlog = "blogs";
let blogId = null;
getBlogTable();
// ReadBlog
// CreateBlog();
// UpdateBlog("e0020222-121e-4c3f-b932-d432bf57e982", "Title Changed", "Author Changed", "Content Changed");
// DeleteBlog("e0020222-121e-4c3f-b932-d432bf57e982");
function ReadBlog() {
  let lst = getBlogs();
  console.log("Read Blog => ", lst);
}

function CreateBlog(title, author, content) {
  let lst = getBlogs();
  const requestModel = {
    id: uuidv4(),
    title: title,
    author: author,
    content: content,
  };
  lst.push(requestModel);
  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);
  SuccessMessage("Create Blog Successful!");
  ClearControl();
  getBlogTable(); // Refresh the table after creating a blog
}

function EditBlog(id) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  console.log("Update Blog => ", items);
  if (items.length === 0) {
    console.log("Update Blog => No Data Found");
    ErrorMessage("No Data Found");
    return;
  }

  const item = items[0];
  blogId = item.id;
  $("#textTitle").val(item.title);
  $("#textAuthor").val(item.author);
  $("#textContent").val(item.content);
  $("#textTitle").focus();
}

function UpdateBlog(id, title, author, content) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  console.log("Update Blog => ", items);
  if (items.length === 0) {
    console.log("Update Blog => No Data Found");
    ErrorMessage("No Data Found");
    return;
  }

  const item = items[0];
  item.title = title;
  item.author = author;
  item.content = content;

  const index = lst.findIndex((x) => x.id === id);
  lst[index] = item;
  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);
  SuccessMessage("Updating Successful");
}

function DeleteBlog(id) {
    let result = confirm("Are you sure you want to delete?");
    if(!result) return;
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  if (items.length === 0) {
    console.log("Delete Blog => No Data Found");
    return;
  }

  lst = lst.filter((x) => x.id !== id);
  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);
  SuccessMessage("Deleting Successful");
  getBlogTable();
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}

function getBlogs() {
  const blogs = localStorage.getItem(tblBlog);
  console.log("Get Blogs => ", blogs);
  let lst = [];
  if (blogs !== null) {
    lst = JSON.parse(blogs);
  }
  return lst;
}

$("#btn_save").click(function () {
  const title = $("#textTitle").val();
  const author = $("#textAuthor").val();
  const content = $("#textContent").val();
  if(blogId === null){
      CreateBlog(title, author, content);
  }else {
    UpdateBlog(blogId,title, author, content);
    blogId = null;
  }
  getBlogTable();
});

function SuccessMessage(message) {
  alert(message);
}
function ErrorMessage(message) {
  alert(message);
}
function ClearControl() {
  $("#textTitle").val("");
  $("#textAuthor").val("");
  $("#textContent").val("");
  $("#textTitle").focus();
}

function getBlogTable() {
  const lst = getBlogs();
  console.log("This is Testing", lst);
  let count = 0;
  let htmlRows = "";
  lst.forEach((item) => {
    let htmlRow = `
          <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="EditBlog('${
                  item.id
                }')">Edit</button>
            </td>
            <td>
                <button type="button" class="btn btn-danger" onclick="DeleteBlog('${
                    item.id
                  }')">Delete</button>
            </td>
              <td>${++count}</td>
              <td>${item.title}</td>
              <td>${item.author}</td>
              <td>${item.content}</td>
          </tr>
          `;
    htmlRows += htmlRow;
  });
  $("#tbody").html(htmlRows);
}
