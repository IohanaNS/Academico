import React from 'react';
import ProfessorLista from './ProfessorLista';
import ProfessorManutencao from './ProfessorManutencao';
import axios from 'axios';
import ProfessorDeletar from './ProfessorDeletar';

class ProfessorCrud extends React.Component {
    constructor(props) {
        super(props);
        this.state = { nome:"",objeto: null, objetos: [], status: CARREGANDO };
    }

    componentDidMount() {
        if (this.state.status === CARREGANDO) {
            this.listarApi();
        }
    }

    voltar = () => {
        this.setState({ status: LISTANDO });
    }
    //salvar
    salvar = (objeto) => {
        console.log("salvar:");
        console.log(objeto);
        if (this.state.status === INCLUINDO)
            this.incluirApi(objeto);
        else
            this.alterarApi(objeto);
    }
    //listar
    listarApi = () => {

        axios.get(`http://localhost:54154/api/professor`)
            .then(result => this.setState({ objetos: result.data, status: LISTANDO }));
    };

    buscaUmApi = (nome) => {
        axios.get(`http://localhost:54154/api/professor/nome?nome=${nome}`)
            .then(result => this.setState({ objetos: result.data, status: LISTANDO }));
    };
    //excluir
    excluirApi = (id) => {
        axios.delete(`http://localhost:54154/api/professor/${id}`)
            .then(result => {
                console.log(result.status);
                this.listarApi();
            });
    };
    excluirObjeto = (id) => {
        this.excluirApi(id);
    }
    alterarApi = (objeto) => {
        axios.put(`http://localhost:54154/api/professor/${objeto.id}`,
            objeto
        )
        .then(result => {
            console.log(result.status);
            this.listarApi();
        });
    };
    incluirApi = (objeto) => {
        axios.post(`http://localhost:54154/api/professor`,
            objeto
        )
        .then(result => {
            console.log(result.status);
            this.listarApi();
        });
    };

    consultar = (objeto) => {
        this.setState({ objeto, status: CONSULTANDO });
    }

    incluir = () => {
        this.setState({ objeto: {}, status: INCLUINDO });
    }

    alterar = (objeto) => {
        this.setState({ objeto, status: ALTERANDO });
    }

    deletar = (objeto) => {
        this.setState({ objeto, status: DELETANDO });
    }

    renderCrud() {
        if (this.state.status === CARREGANDO)
            return <div>Carregando...</div>;
        else if (this.state.status === DELETANDO)
            return <ProfessorDeletar objeto={this.state.objeto} deletar={this.excluirApi} voltar={this.voltar} />
        else if (this.state.status === LISTANDO)
            return <ProfessorLista objetos={this.state.objetos} incluir={this.incluir} alterar={this.alterar} deletar={this.deletar} consultar={this.consultar}/>
        else
            return <ProfessorManutencao objeto={this.state.objeto} consultando={this.state.status === CONSULTANDO} salvar={this.salvar} voltar={this.voltar} />
    }

    render() {
        return (
            <div className="form-group">
                    <label id="nome">Nome</label>
                    <input className="form-control" htmlFor="nome" type="text" name={this.state.nome} onChange={(e) => {
                        this.buscaUmApi(e.target.value )
                    }} />

                {this.renderCrud()}
            </div>
        );
    }
}

const CARREGANDO = 0;
const LISTANDO = 1;
const CONSULTANDO = 2;
const INCLUINDO = 3;
const ALTERANDO = 4;
const DELETANDO = 5;
//const BUSCANDOUM = 6;

export default ProfessorCrud;
