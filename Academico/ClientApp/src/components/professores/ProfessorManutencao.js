import React from 'react';

class ProfessorManutencao extends React.Component {
    constructor(props) {
        super(props);
        this.state = { objeto: this.props.objeto };
    }

    render() {
        return <div>
            <form onSubmit={this.onFormSubmit}>
                <div className="form-group">
                    <label id="matricula">Matrícula</label>
                    <input className="form-control" htmlFor="matricula" readOnly={this.props.consultando} type="text" value={this.state.objeto.matricula}
                        onChange={(e) => {
                            this.setState({ objeto: { ...this.state.objeto, matricula: e.target.value } })
                        }} />
                </div>
                <div className="form-group">
                    <label id="nome">Nome</label>
                    <input className="form-control" htmlFor="nome" readOnly={this.props.consultando} type="text" value={this.state.objeto.nome} onChange={(e) => {
                        this.setState({ objeto: { ...this.state.objeto, nome: e.target.value } })
                    }} />
                </div>
                <div className="form-group">
                    <label id="email">E-mail</label>
                    <input className="form-control" htmlFor="email" readOnly={this.props.consultando} type="text" value={this.state.objeto.email} onChange={(e) => {
                        this.setState({ objeto: { ...this.state.objeto, email: e.target.value } })
                    }} />
                </div>
            </form>
            {this.props.consultando ? null : <button className="btn btn-primary mr-2" disabled={this.props.consultando} onClick={() => this.props.salvar(this.state.objeto)}>Salvar</button>}
            <button className="btn btn-dark" onClick={() => this.props.voltar()}>Voltar</button>
        </div>;
    }
}

export default ProfessorManutencao;