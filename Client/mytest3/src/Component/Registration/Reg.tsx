//import {image} from './RegWindow'

export async function Registration(){
  var names = document.getElementById("name");
  var dates = document.getElementById("date");
  var pass = document.getElementById("password");
  var phone = document.getElementById("phone");
  //var file = document.getElementById("loadFile");
  var citys = document.getElementById("city");
  
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
              password: (pass! as HTMLInputElement).value,
              phone: (phone! as HTMLInputElement).value,
              city: (citys! as HTMLInputElement).value
            })
        });
        // если запрос прошел нормально
        if (response.ok === true) {
          console.log("Status: ", response.status);
          //console.log(image.arrayBuffer);
          //window.location.href = '/Autorization';
          //console.log(image);
        }
        else { // если произошла ошибка, получаем код статуса
            console.log("Status: ", response.status);
            //console.log(image);
        }
    }else{
      console.log("Errors: Data Input incorrect or Not CheckBox");
    }
}

export async function uploadToServer(image: any) {
  // first get our hands on the local file
  const localFile = await fetch(URL.createObjectURL(image));

  // then create a blob out of it (only works with RN 0.54 and above)
  const fileBlob = await localFile.blob();
  //let uint8Array = new Uint8Array(fileBlob);
  //let byteArray = Array.from(uint8Array);
  //console.log(byteArray);

  // then send this blob to filestack
  const serverRes = await fetch('https://localhost:7249/RouteRegistrationFile', { // Your POST endpoint
      method: 'POST',
      headers: {
        'Content-Type': fileBlob && localFile.type
      },
      body:fileBlob
  });
}