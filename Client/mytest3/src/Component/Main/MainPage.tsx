//import { useEffect, useState } from 'react';
import { useCallback, useEffect, useState } from 'react';
import {Pagination} from 'D:/typescript/TypeScript/mytest3/src/Pagination/Pag.tsx';
import { useAppDispatch, useAppSelector } from '../../Redux/Hooks';
import { decrement, increment } from '../../Redux/counterSlice';
import { AddBasket, FormLoadBlock, FormLoadBlockMap } from './LoadBlockTovar';
import "D:/typescript/TypeScript/mytest3/src/Style/Style_MainPanel.css"
import { Token } from '../../Redux/Auth';
import { LoadLabelProfile, LoadRegAuto } from './ProfileLabel';

export interface Ship{
  id : number,
  name : string,
  health : string,
  speed : number,
  flexibility : string,
  teamCrew : number,
  heavyWeapon : number,
  mediumWeapon : number,
  lightWeapon : number,
  price : number,
  imgWay : string
}

  export interface Weapon {
    id : number,
    name : string,
    type : string,
    Speed : number,
    distance : number,
    EffectiveRange : number,
    damage : number,
    price : number
  }
  export interface Crew{
    id : number,
    Shooting : number,
    leadership : number,
    navigation : number,
    Mechanics : number,
    Rigging : number,
    Tracking : number,
    Battle : number,
    price : number
  }

  const ROWS_PER_PAGE = 3;
  
  const getTotalPageCount = (rowCount: number): number =>
    Math.ceil(rowCount / ROWS_PER_PAGE);

export function MainComponent(){
  let body = document.getElementsByTagName('body')[0];
  body.classList.remove("Registr");   //remove the class
  body.classList.remove("Autorize");   //remove the class
  body.classList.remove("Profile");   //remove the class
  body.classList.add("MainComp");   //add the class
  const dispatch = useAppDispatch();
    const [dataShip,setShip] = useState<Ship[]>([]);
    let select = document.getElementById("Items") as HTMLInputElement;
    //const [dataCrew,setCrew] = useState<Crew[]>([]);
    //const [dataWeapons,setWeapons] = useState<Weapon[]>([]);
    //-------------------------------------------------
    var tokenKey = "accessToken";
    const data = sessionStorage.getItem(tokenKey)!;
    let obj: Token = JSON.parse(data);
    let formLabel;
    if(obj!=null){
      formLabel = LoadLabelProfile(obj);
    }else{
      formLabel = LoadRegAuto();
    }
    const [page, setPage] = useState(1);
    var s = useAppSelector((state)=>state.counter.value);
    
    useEffect(() => {
      fetch('https://localhost:7249/ListShip')
              .then((res)=> res.json())
              .then((json) =>setShip(json))
    }, [page,dataShip])

    const reLoad = dataShip.map((item, j) => j < 4 &&(FormLoadBlockMap(item)))
    const handleNextPageClick = useCallback(() => {
        const current = page;
        const next = current + 1;
        const total = dataShip ? getTotalPageCount(dataShip.length) : current;
        dispatch(increment());
        select?.replaceChildren();
        reLoad.splice(0);
        var html = '';
        for (let i=s; i< s+4; i++) {
          if(i<dataShip.length){
            html = FormLoadBlock(dataShip[i],i);
            if (select) select.innerHTML += html;
          }
        } 
        for (let i=s; i< s+4; i++) {
          const lets = document.getElementById("NameShip"+i);
          lets!.addEventListener("click", () => AddBasket(dataShip[i].name));
        }
        setPage(next <= total ? next : current);
      }, [page,dataShip,s]);
      
      const handlePrevPageClick = useCallback(() => {
        const current = page;
        const prev = current - 1;
        dispatch(decrement());
        select?.replaceChildren();
        reLoad.splice(0);
        var html = '';
        for (let i=s; i> s-4; i--) {
          html = FormLoadBlock(dataShip[i],i);
          if (select) select.innerHTML += html;
        } 
        for (let i=s; i> s-4; i--) {
          const lets = document.getElementById("NameShip"+i);
          lets!.addEventListener("click", () => AddBasket(dataShip[i].name));
        }
        setPage(prev > 0 ? prev : current);
      }, [page,dataShip,s]);

	return(
        <>
<div className="FonAbsolute"> 
    <img src="../src/img/Fon-31.png" />
 </div>
<div className="MainComps">
    <header>
        <div className="buton">
            <a href="">Главное меню</a>
            <a href="">Новости</a>
        </div>
      {formLabel}
    </header>
    <div className="MainPanel">
      <div className="mainContainerBlock">
        <div className="ContainerTovar" id="Items">
            {reLoad}
        </div>
        {dataShip && (
        <Pagination
          onNextPageClick={handleNextPageClick}
          onPrevPageClick={handlePrevPageClick}
          disable={{
            left: page === 1,
            right: page === getTotalPageCount(dataShip.length),
          }}
          nav={{ current: page, total: getTotalPageCount(dataShip.length) }}
        />
      )}
      </div>
        <div className="Filters">
            <h3>Фильтр</h3>
            <div className="container">
                <button className="card Ship"><p>Корабли</p></button>
                <button className="card Weapons"><p>Вооружение</p></button>
                <button className="card Crew"><p>Экипаж</p></button>
                <button className="card All"><p>Все</p></button>
            </div>
        </div>
    </div>
    <footer>
        <div className="phone">
            <h4>Контакты</h4>
            <p>Phone: +7 928 938 83 94</p>
        </div>
        <div className="score">
            <h4>Социальные сети</h4>
            <div className="cloud">
                <img src="../src/img/vk.jpg" alt=""/>
                <a href="https://vk.com">ВКонтакте</a>
            </div>
            <div className="cloud">
                <img src="../src/img/telegram.png" alt=""/>
                <a href="https://webk.telegram.org/">Телеграм</a>
            </div>
        </div>
    </footer>
</div>
        </>
	)
}

