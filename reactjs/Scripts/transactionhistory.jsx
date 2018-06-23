class TransactionHistory extends React.Component {

    constructor(props) {
        super(props);
        this.data = props;
    }

    render() {
        return <tr>
            <td><b>Отправил: </b></td>
            <td>{this.data.FromUser}</td>
            <td><b>Получил: </b></td>
            <td>{this.data.ToUser}</td>
            <td><b>Сумма: </b></td>
            <td>{this.data.Amount} $</td>
            <td><b>Дата: </b></td>
            <td>{this.data.DateTime}</td>
        </tr>;
    }
}

class TitleTransactionHistory extends React.Component {

    render() {
        return <h1 className="transHistory">История транзакции</h1>
    }
}
class Back extends React.Component {

    render() {
        return <p className="backcabinet"><a className="registrationLink" href="/"> На главную страницу</a></p>
    }
}
