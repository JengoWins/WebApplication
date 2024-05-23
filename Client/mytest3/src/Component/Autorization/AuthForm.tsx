var tokenKey = "accessToken";

export async function Autorization(){
    var names = document.getElementById("name");
    var pass = document.getElementById("password");
  
    if((pass! as HTMLInputElement).value.length>7){
      console.log("Status: Запуск бека. Уааааааа");
      const response = await fetch("https://localhost:7249/RouteProfile", {
              method: "POST",
              headers: { "Accept": "application/json", "Content-Type": "application/json" },
              body: JSON.stringify({
                Name: (names! as HTMLInputElement).value,
                password: (pass! as HTMLInputElement).value
            })
          });
          // если запрос прошел нормально
          if (response.ok === true) {
            console.log("Status: ", response.status);
            
            const data = await response.json();
            sessionStorage.setItem(tokenKey, JSON.stringify(data));
            console.log("Тест завершен");
            window.location.href = '/Profile';
          }
          else { // если произошла ошибка, получаем код статуса
              console.log("Status: ", response.status);
              console.log(JSON.stringify({
                Name: (names! as HTMLInputElement).value,
                password: (pass! as HTMLInputElement).value
            }));
          }
      }else{
        console.log("Errors: Data Input incorrect or Not CheckBox");
      }
  }