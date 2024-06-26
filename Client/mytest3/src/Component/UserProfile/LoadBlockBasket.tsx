import { Token } from "../../Redux/Auth";
import { Ship } from "../Main/MainPage";


export function FormLoadBasket(Ships:Ship) {
    return (
        <>
    <div className="ElementShip">
        <div className='ImgShip'>
            <img src={Ships.imgWay} />
        </div>
    <div className='TextBox'>
        <div className='Charact'>
            <h5>Характеристики</h5>
            <p>Прочность: {Ships.health}</p>
            <p>Скорость: {Ships.speed}</p>
            <p>Состав команды: {Ships.teamCrew}</p>
        </div>
        <div className='Status'>
            <h5>Вооружение</h5>
            <p>Тяжелые пушки: {Ships.heavyWeapon}</p>
            <p>Срдение пушки: {Ships.mediumWeapon}</p>
          <p>Легкие пушки: {Ships.lightWeapon}</p>
        </div>
    </div>
    <div className='rate'>
        <p>Цена: {Ships.price}</p>
        <button className="FixButton" onClick={()=> DeleteBasket(Ships.name)}>Удалить</button>
    </div>
        </div>
</>
    )
}

var tokenKey = "accessToken";

export async function DeleteBasket(str: string) {
    const data = sessionStorage.getItem(tokenKey)!;
    let obj: Token = JSON.parse(data);
    const response = await fetch("https://localhost:7249/DeleteShipBasket", {
        method: "DELETE",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            nameShip: str,
            nameUser: obj.username
      })
    });
    if (response.ok === true) {
        console.log("Status: ", response.status);
        console.log("Status: ", "Cъела рыбка");
      }
      else { 
        console.log("Status: ", response.status);
        console.log("Status: ", "Не клюет");
      console.log(str);
      }
}

export async function ExitAccount(){
    sessionStorage.clear();
}


export async function AddInGame(price:number){
    const data = sessionStorage.getItem(tokenKey)!;
    let obj: Token = JSON.parse(data);
    const response = await fetch("https://localhost:7249/ShipBasketInGame", {
        method: "DELETE",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            PriceTotal: price,
            nameUser: obj.username
      })
    });
    if (response.ok === true) {
        console.log("Status: ", response.status);
        console.log("Status: ", "Cъела рыбка");
      }
      else { 
        console.log("Status: ", response.status);
        console.log("Status: ", "Не клюет");
      console.log(price);
      }
}

