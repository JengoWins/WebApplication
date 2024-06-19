
import { login } from "../../Redux/Auth";
import { useAppDispatch } from "../../Redux/Hooks";
import { Autorization } from "./AuthForm";
import "D:/typescript/TypeScript/mytest3/src/Style/auto.css";

export function FormAuto() {
  let body = document.getElementsByTagName('body')[0];
  body.classList.remove("Registr");   //remove the class
  body.classList.remove("MainComp");   //remove the class
  body.classList.remove("Profile");   //remove the class
  body.classList.add("Autorize");   //add the class
    const dispatch = useAppDispatch();
  return (
    <>
    <div className="BlockAuto">
      <div className="NavMenuBlock">
            <a href="/Registration">Регистрация</a>
      </div>
      <div className="Form-a">
          <h2> Вход </h2>
          {/*<form action="/Profile" method="post">*/}
            <input type="text" name="name" id="name" placeholder="ФИО"/>
            <input type="password" name="password" id="password" placeholder="Пароль"/>
            <input type="submit" value="Вход" className="button" onClick={()=> {Autorization(),dispatch(login())}}/>
          {/*</form>*/}
    </div>
</div>
    </>
  );
}
