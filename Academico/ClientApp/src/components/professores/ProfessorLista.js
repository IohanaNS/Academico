import React from 'react';

class ProfessorLista extends React.Component {
    constructor(props) {
        super(props);
    }

    renderLista() {
        return (
             
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Matrícula</th>
                        <th>Nome</th>
                        <th>E-mail</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.props.objetos.map(obj =>
                            <tr key={obj.id}>
                                <td>{obj.matricula}</td>
                                <td>{obj.nome}</td>
                                <td>{obj.email}</td>
                                <td>
                                    <button className="btn btn-primary mr-2" onClick={() => this.props.consultar(obj)}>Consultar</button>
                                    <button className="btn btn-success mr-2" onClick={this.props.incluir}>Incluir</button>
                                    <button className="btn btn-warning mr-2" onClick={() => this.props.alterar(obj)}>Alterar</button>
                                    <button className="btn btn-danger mr-2" onClick={() => this.props.deletar(obj)}>Excluir</button>
                                </td>
                            </tr>
                        )
                    }
                </tbody>
            </table>
        );
    }

    render() {
        return (
            <div>

                {this.renderLista()}
            </div>
        );
    }
}

export default ProfessorLista;