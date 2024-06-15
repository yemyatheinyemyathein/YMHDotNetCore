const tblBlog = "blogs";
let blogId = null;

const readBlog = () => {
  const blogs = localStorage.getItem(tblBlog);
  console.log(blogs);
};

const editBlog = (id) => {
  let lst = getBlogs();

  const item = lst.filter((x) => x.id === id)[0];

  if (!item) {
    errorMessage("No data found with that id.");
    return;
  }

  blogId = item.id;
  $("#txtTitle").val(item.title);
  $("#txtAuthor").val(item.author);
  $("#txtContent").val(item.content);
  $("#txtTitle").focus();
};

const createBlog = (title, author, content) => {
  let lst = getBlogs();

  const requestModel = {
    id: lst.length > 0 ? lst[lst.length - 1].id + 1 : 1,
    title,
    author,
    content,
  };

  lst.push(requestModel);

  const jsonBlog = JSON.stringify(lst);

  localStorage.setItem(tblBlog, jsonBlog);

  successMessage("Saving successful!");
  clearControls();
};

const updateBlog = (id, title, author, content) => {
  let lst = getBlogs();

  const item = lst.filter((x) => x.id === id)[0];

  if (!item) {
    errorMessage("No data found with that id.");
    return;
  }

  item.title = title;
  item.author = author;
  item.content = content;

  const index = lst.findIndex((x) => x.id === id);
  lst[index] = item;

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);

  successMessage("Updating successful!");
};

const deleteBlog = (id) => {
  confirmMessage("Are you sure you want to delete?").then((val) => {
    let lst = getBlogs();

    const removedLst = lst.filter((x) => x.id !== id);

    const jsonBlog = JSON.stringify(removedLst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Blog deleted successfully!");

    getBlogTable();
  });
};

const getBlogs = () => {
  const blogs = localStorage.getItem(tblBlog);

  let lst = [];

  if (blogs !== null) lst = JSON.parse(blogs);

  return lst;
};

// createBlog("test title", "test author", "test content");
// readBlog();
// updateBlog(2, "PUT title", "PUT author", "PUT content");
// deleteBlog(1);

// JQuery
$("#btnSave").click(() => {
  const title = $("#txtTitle").val();
  const author = $("#txtAuthor").val();
  const content = $("#txtContent").val();

  if (blogId === null) {
    createBlog(title, author, content);
  } else {
    updateBlog(blogId, title, author, content);
    blogId = null; // refreshes id state
  }

  getBlogTable();
});

const clearControls = () => {
  $("#txtTitle").val("");
  $("#txtAuthor").val("");
  $("#txtContent").val("");
  $("#txtTitle").focus();
};

const getBlogTable = () => {
  const lst = getBlogs();
  let htmlRows = "";

  lst.forEach((item) => {
    const htmlRow = `
    <tr>
      <td>
        <button type="button" class="btn btn-warning" onClick="editBlog(${item.id})">Edit</button>
        <button type="button" class="btn btn-danger" onClick="deleteBlog(${item.id})">Delete</button>
      </td>
      <td>${item.id}</td>
      <td>${item.title}</td>
      <td>${item.author}</td>
      <td>${item.content}</td>
    </tr>
    `;

    htmlRows += htmlRow;
  });

  $("#tbody").html(htmlRows);
};

getBlogTable();