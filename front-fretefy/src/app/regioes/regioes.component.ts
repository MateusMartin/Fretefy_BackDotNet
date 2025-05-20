import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegiaoService, Regiao, Cidade } from '../services/regiao.service';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-regioes',
  standalone: true,
  imports:
    [
      CommonModule,
      TableModule,
      InputTextModule,
      CheckboxModule,
      FormsModule,
      ButtonModule,
      DialogModule
    ],
  templateUrl: './regioes.component.html',
  styleUrl: './regioes.component.scss'
})
export class RegioesComponent implements OnInit {
  regioes: Regiao[] = [];
  regioesFiltradas: Regiao[] = [];
  filtroNome: string = '';
  exibirInativas: boolean = false;
  modalVisivel: boolean = false;
  cidadesDaRegiao: Cidade[] = [];
  nomeDaRegiaoSelecionada: string = '';

  constructor(private regiaoService: RegiaoService, private router: Router) { }

  ngOnInit() {
    this.regiaoService.obterRegioes().subscribe({
      next: (dados) => {
        this.regioes = dados;
        this.filtrarRegioes();
      },
      error: (erro) => console.error('Erro ao buscar regiões:', erro)
    });
  }

  filtrarRegioes() {
    const termo = this.filtroNome.toLowerCase();
    this.regioesFiltradas = this.regioes.filter(regiao => {
      const nomeOk = regiao.nome.toLowerCase().includes(termo);
      const ativaOk = this.exibirInativas || regiao.ativa;
      return nomeOk && ativaOk;
    });
  }

  verCidades(id: string) {
    const regiao = this.regioes.find(r => r.id === id);
    if (regiao && regiao.cidades) {
      this.cidadesDaRegiao = regiao.cidades;
      this.nomeDaRegiaoSelecionada = regiao.nome;
      this.modalVisivel = true;
    } else {
      console.warn('Região não encontrada ou sem cidades:', id);
    }
  }
  editar(id: number) {
    console.log('Editar região', id);
    // Implementar lógica de edição
  }

  inativar(id: number) {
    console.log('Inativar região', id);
    // Implementar a chamada para inativar a região
  }

  deletar(id: number) {
    console.log('Deletar região', id);
    // Implementar a chamada para deletar a região
  }

  criarNovaRegiao() {
    this.router.navigate(['/regioes/nova']);
  }
}
