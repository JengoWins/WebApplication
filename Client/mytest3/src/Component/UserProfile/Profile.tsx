
import { useEffect, useState } from "react";
import { Ship } from "../Main/MainPage";
import "D:/typescript/TypeScript/mytest3/src/Style/Style_User.css";
import { AddInGame, ExitAccount, FormLoadBasket } from "./LoadBlockBasket";
import { Token } from "../../Redux/Auth";
import Modal from "react-modal";

export interface Person1{
  name: string,
  date: string,
  phone: string,
  city: string,
  photo: Uint8Array
}

var tokenKey = "accessToken";

export function Profile() {
  let body = document.getElementsByTagName('body')[0];
  body.classList.remove("Registr");   //remove the class
  body.classList.remove("MainComp");   //remove the class
  body.classList.remove("Autorize");   //remove the class
  body.classList.add("Profile");   //add the class

  const [modalIsOpen, setModalIsOpen] = useState(false);

  const customStyles = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-50%',
      transform: 'translate(-50%, -50%)',
      width: "750px",
      height: "150px",
      padding: "15px"
    },
  };

  const data = sessionStorage.getItem(tokenKey)!;
  let obj: Token = JSON.parse(data);
  
  const [dataShip,setShip] = useState<Ship[]>([]);
    useEffect(() => {
      fetch('https://localhost:7249/ListBasket/'+obj.username)
              .then((res)=> res.json())
              .then((json) =>setShip(json))
    }, [])

    const [userFrom,setUser] = useState<Person1>();
    useEffect(() => {
      fetch('https://localhost:7249/PersonProfile/'+obj.username)
              .then((res)=> res.json())
              .then((json) =>setUser(json))
    }, [])

  //var arrayBufferView = new Uint8Array( userFrom?.photo );
  // blob = new Blob( [ userFrom!.photo ], { type: "image/jpeg" } );
  //var urlCreator = window.URL || window.webkitURL;
  //var imageUrl = urlCreator.createObjectURL( blob );
  //console.log(userFrom);
  //console.log(userFrom?.name);
  const reLoad = dataShip.map((item) => FormLoadBasket(item))
  
  const countPrice:number = dataShip.reduce((a,b) =>  a = a + b.price , 0 )

  const modalContent = (
    <div className="modal-Price">
      <h2>Подтверждение покупки</h2>
      <p>Сумма корзины составляет {countPrice} кредитов. Уверены в своей покупке?</p>
      <button id="AddInGame" onClick={()=>{setModalIsOpen(true),AddInGame(countPrice)}}>Покупка</button>
      <button id="CloseInGame" onClick={()=>setModalIsOpen(false)}>Закрыть</button>
    </div>
  );

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
            <img src="" alt=""/>
            </div>
          <div className="Text">
              <p className="Sillet"><b>ФИО:</b> {userFrom?.name}</p>
              <p><b>Дата Рождения:</b> {userFrom?.date} </p>
              <p className="Role_User"><b>Роль:</b> Пользователь</p>
              <p className="Sillet"><b>Город:</b> {userFrom?.city}</p>
              <p><b>Телефон:</b> {userFrom?.phone}</p>
          </div>
      </div>
      <br />
      <br />
      <h1>Корзина</h1>
      <div className="ContainersTovar">
      {reLoad}
      </div>
      <div className="Complete">
        <button onClick={()=>setModalIsOpen(true)}>Оформить заказ</button>
        <Modal style={customStyles} isOpen={modalIsOpen} onRequestClose={()=>setModalIsOpen(false)}>
            {modalContent}
        </Modal>
      </div>
    </div>
    </>
  );
}