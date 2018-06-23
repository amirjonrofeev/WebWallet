class MenuBar extends React.Component {
    render() {
        return <div className="menubarSecond">
            <p className="MLogoText"><a href="/" className="logotext">WEBWALLET.COM</a></p>
        </div>;
    }
}

class RegistrationForm extends React.Component {

    render() {
        return <div className="registrationForm">
            <p><b>После регистрации вы можете пользововаться всеми функциями сайта.</b></p>
            <p><b>Заполните все <u>пустые поля!</u></b></p>
            <p><input name="Login" id="Login" className="registrationBox" type="text" placeholder="Введите свой логин" /></p>
            <p><input name="FirstName" id="FirstName" className="registrationBox" type="text" placeholder="Введите свое имя" /></p>
            <p><input name="MiddleName" id="MiddleName" className="registrationBox" type="text" placeholder="Введите свою фамилию" /></p>
            <p><input name="LastName" id="LastName" className="registrationBox" type="text" placeholder="Введите свое отчество" /></p>
            <p><input name="Email" id="Email" className="registrationBox" type="text" placeholder="Введите свой email" /></p>
            <p><input name="Password" id="Password" className="registrationBox" type="password" placeholder="Введите пароль" /></p>
            <p><input name="RepeatPassword" id="RepeatPassword" className="registrationBox" type="password" placeholder="Введите пароль заново" /></p>
            <p><input className="registrationSubmit" type="submit" value="Завершить регистрацию" /></p>
            <p><a href="/Home/Login" className="registrationLink">Вернутся назад.</a>  <a href="Registration" className="registrationLink">Очистить поля</a></p>
        </div>;
    }
}
