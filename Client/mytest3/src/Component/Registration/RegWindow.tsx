import { useState } from 'react';
import { Registration, uploadToServer } from './Reg';
import "D:/typescript/TypeScript/mytest3/src/Style/reg.css";



export function FormReg() {
  let body = document.getElementsByTagName('body')[0];
  body.classList.remove("MainComp");   //remove the class
  body.classList.remove("Autorize");   //remove the class
  body.classList.remove("Profile");   //remove the class
  body.classList.add("Registr");   //add the class

  var [image, setImage] = useState(Object||null)
  var [imageSave, setImageFile] = useState(Object||null)
  
  const onImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];
      setImageFile(file);
      setImage(URL.createObjectURL(file));
    }
  }

  return (
    <>
    <div className="BlockAuto">
      <div className="NavMenuBlock">
            <a href="/Autorization">Авторизация</a>
      </div>
      <div className="Form-a">
          <h2> Вход </h2>
          {/*<form action="/Autorization" method="post">*/}
          <input type="text" id='name' name="name" placeholder="Фамилия Имя Отчество" pattern="^[А-Яа-яЁё\s]+$" title="Введите корректно фио" required />
          <input type="datetime-local" id="date" name="date" required/>
          <img id="photo" src={image}/>
          <input type="file" id="loadFile" name="loadFile" onChange={onImageChange} required/>
          <input type="tel" id="phone" name="phone" required/>
          <input type="text" id="city" name="city" required/>
          <input type="password" id='password' name="password" placeholder="Пароль" required />
          <input type="password" id='pass2' placeholder="Повтор пароля" required/>
          <input type="checkbox" id='check' required /> Согласие над  <a href="#">пользовательским  соглашением </a><br/>
          <div className="Udo" id="Udo"></div>
            <button className="button" id="submitLogin" onClick={()=> {Registration(); uploadToServer(imageSave);}}>Зарегистрироваться</button>
          {/*</form>*/}
     </div>
  </div>
    </>
  );
  
}

