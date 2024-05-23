export async function Registration(){
  var names = document.getElementById("name");
  var dates = document.getElementById("date");
  var pass = document.getElementById("password");
  var pass2 = document.getElementById("pass2");
  var checkBox = document.getElementById("check");
  if((pass! as HTMLInputElement).value===(pass2! as HTMLInputElement).value && (checkBox! as HTMLInputElement).checked === true && (pass! as HTMLInputElement).value.length>7){
    console.log("Status: Запуск бека. Уааааааа");
    const response = await fetch("https://localhost:7249/RouteRegistration", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
              name: (names! as HTMLInputElement).value,
              date: (dates! as HTMLInputElement).value,
              password: (pass! as HTMLInputElement).value
            })
        });
        // если запрос прошел нормально
        if (response.ok === true) {
          console.log("Status: ", response.status);
          window.location.href = '/Autorization';
        }
        else { // если произошла ошибка, получаем код статуса
            console.log("Status: ", response.status);
        }
    }else{
      console.log("Errors: Data Input incorrect or Not CheckBox");
    }
}