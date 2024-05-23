import { Token } from "../../Redux/Auth"

export function LoadLabelProfile(obj:Token) {
    return (
        <>
        <div className="entrance">
          <a href="/Profile">{obj.username}</a>
          <img src="../src/img/img1.jpg" alt=""/>
        </div>
        </>
    )
}

export function LoadRegAuto() {
    return (
        <>
        <div className="entrance">
            <a href="/Autorization">Авторизация</a>
            <a href="/Registration">Регистрация</a>
        </div>
        </>
    )
}