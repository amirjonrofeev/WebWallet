class Login extends React.Component {

    render() {
        return <div className="login">
            <p>Пройдите <a className="registrationLink" href="/Home/Registration">регистрацию</a> если у вас нет аккаунта.</p>
            <p><input name="Login" id="Login" placeholder="Вводите свой логин" className="boxarea" type="text" /></p>
            <p><input name="Password" id="Password" placeholder="Вводите свой пароль" className="boxarea" type="password" /></p>
            <p><input className="loginbutton" type="submit" value="Войти" /></p>
        </div>;
    }
}

class MenuBarLogin extends React.Component {
    render() {
        return <div className="menubar">
            <p className="MLogoText"><a href="/" className="logotext">WEBWALLET.COM</a></p>
        </div>;
    }
}

class Footer extends React.Component {
    render() {
        return <div className="footerPart">
            <p className="information">Наш сервис один из самых лучших сервисов в мире. Мы всегда рады новым пользователям! Хотите больше узнать
                        о своем аккаунте тогда зайдите в раздел <a className="registrationLink" href="">Информация о пользователе</a>
                , в разделе вы можете увидеть специальный хеш - кошелка. Наш сервис поддерживает отправку денег через счет, для этого у нас
                        есть раздел <a className="registrationLink" href="">Перевести деньги</a>, проста указывайте нужного вам пользователья и отправляйте сумму.
                        Не помните кому сколько отправили? Тогда у нас есть раздел <a className="registrationLink" href="">История
                    транзакции</a>. В разделе есть весь список транзакции, когда и кому вы отправляли деньги. Если вам всё ещё не
                понятно то у нас есть раздел <a className="registrationLink" href="">Справка</a> который вам поможет.</p>
            <a href="/" className="sitename"><b>WebWallet 2018</b></a>
        </div>;
    }
}