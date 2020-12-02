import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import AlunoCrud from './components/alunos/AlunoCrud';
import ProfessorCrud from './components/professores/ProfessorCrud';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/alunos' component={AlunoCrud} />
        <Route path='/professores' component={ProfessorCrud} />
      </Layout>
    );
  }
}
