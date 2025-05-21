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
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';


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
      DialogModule,
      ToastModule,
      ConfirmDialogModule
    ],
  providers: [MessageService, ConfirmationService], // Providencia o MessageService
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

  constructor(
    private regiaoService: RegiaoService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit() {
    this.carregarRegioes();
  }

  carregarRegioes() {
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

  editar(id: string) {
    this.router.navigate(['/regioes/editar', id]);
  }


  confirmarDownloadExcel(): void {
    this.confirmationService.confirm({
      message: 'Deseja incluir as regiões inativas no Excel?',
      header: 'Exportar Regiões',
      icon: 'pi pi-question-circle',
      acceptLabel: 'Sim',
      rejectLabel: 'Não',
      accept: () => {
        this.regiaoService.baixarExcel("", true);
      },
      reject: () => {
        this.regiaoService.baixarExcel("", false);
      }
    });
  }



  inativar(id: number) {
    const idStr = id.toString();
    this.regiaoService.inativarRegiao(idStr).subscribe({
      next: () => {
        this.carregarRegioes();
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: 'Região inativada com sucesso!',
          life: 2000
        });
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: err?.error?.message || 'Falha ao inativar região',
          life: 3000
        });
      }
    });
  }



  ativar(id: number) {
    const idStr = id.toString();
    this.regiaoService.ativarRegiao(idStr).subscribe({
      next: () => {
        this.carregarRegioes();
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: 'Região ativada com sucesso!',
          life: 2000
        });
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: err?.error?.message || 'Falha ao ativada região',
          life: 3000
        });
      }
    });
  }



  deletar(id: number) {
    const idStr = id.toString();
    this.regiaoService.removerRegiao(idStr).subscribe({
      next: () => {
        this.carregarRegioes();
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: 'Região Removida com sucesso!',
          life: 2000
        });
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: err?.error?.message || 'Falha ao inativar região',
          life: 3000
        });
      }
    });
  }

  criarNovaRegiao() {
    this.router.navigate(['/regioes/nova']);
  }
}
