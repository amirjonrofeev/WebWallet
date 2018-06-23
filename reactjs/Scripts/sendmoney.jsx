class SendMoney extends React.Component {

    constructor(props) {
        super(props);
        this.data = props;
    }

    render() {
        return <table>
            <tr>
                <td className="userTable">
                    <p><img width="128px" height="128px" src="/Contents/Images/user.png" /></p>
                    <p>Здравствуйте <b>{this.data.UserName}</b> !</p>
                    <p>Счёт: <b>{this.data.Amount}</b>$</p>
                    <p><a className="registrationLink" href="/Cabinet/">Главная страница</a></p>
                    <p><a className="registrationLink" href="/Cabinet/About/">Информация о пользователе</a></p>
                    <p><a className="registrationLink" href="/Cabinet/FullMoney/">Пополнить счёт</a></p>
                    <p><a className="registrationLink" href="/Cabinet/SendMoney/">Перевести деньги</a></p>
                    <p><a className="registrationLink" href="/Cabinet/TransactionHistory">История транзакции</a></p>
                    <p><a className="registrationLink" href="/Cabinet/Support">Справка</a></p>
                    <p><a className="registrationLink" href="/Home/Login">Выйти</a></p>
                </td>
                <td className="userInformation">
                    <p><h1>Перевести деньги</h1></p>
                    <p>Укажите адресс кошелка того кому вы хотите перенисти сумму.</p>
                    <p><input name="WalletId" id= "WalletId" type="text" className="registrationBox" placeholder="Адресс кошелка" /></p>
                    <p>Укажите сумму которую вы хотите отправить.</p>
                    <p><input name="Amount" id= "Amount" type="tel" className="registrationBox" placeholder="Сумма" /></p>
                    <p><input className="registrationSubmit" type="submit" value="Перевод денег" /></p>
                </td>
            </tr>
        </table>;
    }
}

class FullMoney extends React.Component {

    constructor(props) {
        super(props);
        this.data = props;
    }

    render() {
        return <table>
            <tr>
                <td className="userTable">
                    <p><img width="128px" height="128px" src="/Contents/Images/user.png" /></p>
                    <p>Здравствуйте <b>{this.data.UserName}</b> !</p>
                    <p>Счёт: <b>{this.data.Amount}</b>$</p>
                    <p><a className="registrationLink" href="/Cabinet/">Главная страница</a></p>
                    <p><a className="registrationLink" href="/Cabinet/About/">Информация о пользователе</a></p>
                    <p><a className="registrationLink" href="/Cabinet/FullMoney/">Пополнить счёт</a></p>
                    <p><a className="registrationLink" href="/Cabinet/SendMoney/">Перевести деньги</a></p>
                    <p><a className="registrationLink" href="/Cabinet/TransactionHistory">История транзакции</a></p>
                    <p><a className="registrationLink" href="">Справка</a></p>
                    <p><a className="registrationLink" href="/Home/Login">Выйти</a></p>
                </td>
                <td className="userInformation">
                    <p><h1>Пополнить счёт</h1></p>
                    <p>Укажите сумму которую вы хотите получить.</p>
                    <p><input name="Amount" id="Amount" type="tel" className="registrationBox" placeholder="Сумма" /></p>
                    <p><input className="registrationSubmit" type="submit" value="Пополнить счёт" /></p>
                </td>
            </tr>
        </table>;
    }
}