import { Token } from "../../Redux/Auth";
import { Ship } from "./MainPage";

export function FormLoadBlock(Ships:Ship,index:number): string {
    var block = `<div class='ContainerShip'>
    <h4>${Ships.name}</h4>
    <div class='imgShip'>
       <img src=${Ships.imgWay} />
    </div>
    <div class='Descript'>
        <div class='Charact'>
            <h5>Характеристики</h5>
            <p>Прочность: ${Ships.health}</p>
            <p>Скорость: ${Ships.speed}</p>
            <p>Состав команды: ${Ships.teamCrew}</p>
        </div>
        <div class='Status'>
            <h5>Вооружение</h5>
        <p>Тяжелые пушки: ${Ships.heavyWeapon}</p>
        <p>Срдение пушки: ${Ships.mediumWeapon}</p>
        <p>Легкие пушки: ${Ships.lightWeapon}</p>
        </div>
    </div>
    <div class='Price'>
        <p>Цена:${Ships.price}</p>
    </div>
    <button id='NameShip${index}' value='Корзина'>В корзину</button>
</div>`;
    return block;
}

export function FormLoadBlockMap(Ships:Ship) {
    return (
        <>
    <div className='ContainerShip'>
    <h4>{Ships.name}</h4>
    <div className='imgShip'>
       <img src={Ships.imgWay} />
    </div>
    <div className='Descript'>
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
    <div className='Price'>
        <p>Цена: {Ships.price}</p>
    </div>
    <button id='NameShip' onClick={()=> AddBasket(Ships.name)}>В корзину</button>
</div>
</>
    )
}

var tokenKey = "accessToken";

export async function AddBasket(str: string) {
    const data = sessionStorage.getItem(tokenKey)!;
    let obj: Token = JSON.parse(data);
    const response = await fetch("https://localhost:7249/AddShipBasket", {
        method: "POST",
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
