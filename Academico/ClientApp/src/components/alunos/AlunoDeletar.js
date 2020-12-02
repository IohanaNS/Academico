import React from 'react';

class AlunoDeletar extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <div>
            <form onSubmit={this.onFormSubmit}>
                <div className="form-group">
                    <label id="matricula">Matrícula</label>
                    <input className="form-control" htmlFor="matricula" readOnly={true} type="text" value={this.props.objeto.matricula} />
                </div>
                <div className="form-group">
                    <label id="nome">Nome</label>
                    <input className="form-control" htmlFor="nome" readOnly={true} type="text" value={this.props.objeto.nome} />
                </div>
            </form>
            <div>
                <strong>Deseja excluir o aluno acima?</strong>
            </div>
            <button className="btn btn-danger mr-2" onClick={() => this.props.deletar(this.props.objeto.id)}>Apagar</button>
            <button className="btn btn-dark" onClick={() => this.props.voltar()}>Voltar</button>
        </div>;
    }
}

export default AlunoDeletar;