
import { useEffect, useState } from "react";
import { Ship } from "../Main/MainPage";
import "D:/typescript/TypeScript/mytest3/src/Style/Style_User.css";
import { ExitAccount, FormLoadBasket } from "./LoadBlockBasket";
import { Token } from "../../Redux/Auth";

var tokenKey = "accessToken";

export function Profile() {
  let body = document.getElementsByTagName('body')[0];
  body.classList.remove("Registr");   //remove the class
  body.classList.remove("MainComp");   //remove the class
  body.classList.remove("Autorize");   //remove the class
  body.classList.add("Profile");   //add the class

  const data = sessionStorage.getItem(tokenKey)!;
  let obj: Token = JSON.parse(data);
  
  const [dataShip,setShip] = useState<Ship[]>([]);
    useEffect(() => {
      fetch('https://localhost:7249/ListBasket')
              .then((res)=> res.json())
              .then((json) =>setShip(json))
    }, [])
  const reLoad = dataShip.map((item) => FormLoadBasket(item))
  
  return (
    <>
     <div className="main">
      <h1>Личный кабинет</h1>
      <div className="buttons">
          <a href="/">Главное меню</a>
          <a href="/" id="Exit" onClick={()=> ExitAccount()}>Выход</a>
      </div>
      <h2>Ваши персональные данные</h2>
      <div className="profit">
          <div className="Images">
            <img src="../src/img/img1.jpg" alt=""/>
            </div>
          <div className="Text">
              <p className="Sillet"><b>ФИО:</b> {obj.username}</p>
              <p><b>Дата Рождения:</b> {obj.date}</p>
              <p className="Sillet"><b>Логин:</b> Silver1277</p>
              <p className="Role_User"><b>Роль:</b> Пользователь</p>
              <p className="Sillet"><b>Город:</b> Городок Лайберситу</p>
              <p><b>Телефон:</b> 8-800-647-77-77</p>
          </div>
      </div>
      <br />
      <br />
      <h1>Корзина</h1>
      <div className="ContainersTovar">
      {reLoad}
      </div>
      <button className="Complete">Оформить заказ</button>
    </div>
    </>
  );
}