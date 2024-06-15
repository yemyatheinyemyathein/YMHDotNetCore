const successMessage = (message) => {
    /* sweetalert approach */
    //   Swal.fire({
    //     title: "Success!",
    //     text: message,
    //     icon: "success",
    //   });
  
    /* notiflix approach */
    Notiflix.Report.success("Success!", message, "Okay");
  };
  
  const errorMessage = (message) => {
    //   Swal.fire({
    //     title: "Error!",
    //     text: message,
    //     icon: "error",
    //   });
  
    Notiflix.Report.failure("Error!", message, "Okay");
  };
  
  const confirmMessage = (message) => {
    /* sweetalert approach */
    //   let confirmMessageResult = new Promise((success, error) => {
    //     Swal.fire({
    //       title: message,
    //       text: "You won't be able to revert this!",
    //       icon: "warning",
    //       showCancelButton: true,
    //       confirmButtonColor: "purple",
    //       cancelButtonColor: "red",
    //       confirmButtonText: "Yes, delete it!",
    //     }).then((result) => {
    //       if (result.isConfirmed) {
    //         success();
    //       } else {
    //         error();
    //       }
    //     });
    //   });
  
    /* notiflix approach */
    let confirmMessageResult = new Promise((success, error) =>
      Notiflix.Confirm.show(
        message,
        "You won't be able to revert this!",
        "Yes, delete it",
        "Cancel",
        function okCb() {
          success();
        },
        function cancelCb() {
          error();
        }
      )
    );
  
    return confirmMessageResult;
  };
  
  const getProduct = () => {
    const products = localStorage.getItem(tblProducts);
    let lst = [];
  
    if (products !== null) lst = JSON.parse(products);
  
    return lst;
  };
  
  const getCart = () => {
    const carts = localStorage.getItem(tblCart);
    let lst = [];
  
    if (carts !== null) lst = JSON.parse(carts);
  
    return lst;
  };