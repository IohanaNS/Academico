import React from 'react';
import AlunoLista from './AlunoLista';
import AlunoManutencao from './AlunoManutencao';
import axios from 'axios';
import AlunoDeletar from './AlunoDeletar';

class AlunoCrud extends React.Component {
    constructor(props) {
        super(props);
        this.state = { objeto: null, objetos: [], status: CARREGANDO };
    }

    componentDidMount() {
        if (this.state.status === CARREGANDO) {
            this.listarApi();
        }
    }

    voltar = () => {
        this.setState({ status: LISTANDO });
    }

    salvar = (objeto) => {
        console.log("salvar:");
        console.log(objeto);
        if (this.state.status === INCLUINDO)
            this.incluirApi(objeto);
        else
            this.alterarApi(objeto);
    }

    listarApi = () => {
        axios.get('http://localhost:54154/api/aluno')
        .then(result => this.setState({ objetos: result.data, status: LISTANDO }));
    };

    excluirApi = (id) => {
        axios.delete(`http://localhost:54154/api/aluno/${id}`)
        .then(result => {
            console.log(result.status);
            this.listarApi();
        });
    };

    excluirObjeto = (id) => {
        this.excluirApi(id);
    }

    alterarApi = (objeto) => {
        axios.put(`http://localhost:54154/api/aluno/${objeto.id}`,
            objeto
        )
        .then(result => {
            console.log(result.status);
            this.listarApi();
        });
    };

    incluirApi = (objeto) => {
        axios.post('http://localhost:54154/api/aluno',
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
            return <AlunoDeletar objeto={this.state.objeto} deletar={this.excluirApi} voltar={this.voltar} />
        else if (this.state.status === LISTANDO)
            return <AlunoLista objetos={this.state.objetos} incluir={this.incluir} alterar={this.alterar} deletar={this.deletar} consultar={this.consultar} />;
        else
            return <AlunoManutencao objeto={this.state.objeto} consultando={this.state.status === CONSULTANDO} salvar={this.salvar} voltar={this.voltar} />
    }

    render() {
        return (
            <div>
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

export default AlunoCrud;
