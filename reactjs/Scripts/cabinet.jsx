class Cabinet extends React.Component {

    constructor(props) {
        super(props);
        this.data = props;
    }

    render() {
        return <table>
            <tr>
                <td className="userTable">
                    <p><img width="128px" height="128px" src="/Contents/Images/user.png" /></p>
                    <p>Здравствуйте <b>{this.data.FirstName}</b> !</p>
                    <p>Счёт: <b>{this.data.Money}</b>$</p>
                    <p><a className="registrationLink" href="/Cabinet/">Главная страница</a></p>
                    <p><a className="registrationLink" href="/Cabinet/About/">Информация о пользователе</a></p>
                    <p><a className="registrationLink" href="/Cabinet/FullMoney/">Пополнить счёт</a></p>
                    <p><a className="registrationLink" href="/Cabinet/SendMoney/">Перевести деньги</a></p>
                    <p><a className="registrationLink" href="/Cabinet/TransactionHistory">История транзакции</a></p>
                    <p><a className="registrationLink" href="/Cabinet/Support">Справка</a></p>
                    <p><a className="registrationLink" href="/Home/Login">Выйти</a></p>
                </td>
                <td className="userInformation">
                    <p className="userInformationText">Наш сервис один из самых лучших сервисов в мире. Мы всегда рады новым пользователям! Хотите больше узнать
                        о своем аккаунте тогда зайдите в раздел <a className="registrationLink" href="">Информация обо мне.</a>
                        , в разделе вы можете увидеть специальный хеш - кошелка. Наш сервис поддерживает отправку денег через счет, для этого у нас
                        есть раздел <a className="registrationLink" href="">Перевести деньги</a>, проста указывайте нужного вам пользователья и отправляйте сумму.
                        Не помните кому сколько отправили? Тогда у нас есть раздел <a className="registrationLink" href="">История транзакции</a>. В разделе есть весь
                        список транзакции, когда и кому вы отправляли деньги. Если вам всё ещё не понятно то у нас есть раздел <a className="registrationLink" href="">Справка</a> который вам поможет.</p>
                    <p>
                        <a id={this.data.FirstName} href="Cabinet/About" ><img className="supportImg" src="/Contents/Images/about.png" /></a>
                        <a href="Cabinet/SendMoney"><img className="supportImg" src="/Contents/Images/money.png" /></a>
                        <a href=""><img className="supportImg" src="/Contents/Images/history.png" /></a>
                        <a href=""><img className="supportImg" src="/Contents/Images/manual.png" /></a>
                    </p>
                </td>
            </tr>
        </table>;
    }
}
