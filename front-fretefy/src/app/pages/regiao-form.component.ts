import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RegiaoService, Regiao, Cidade } from '../services/regiao.service';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';





@Component({
  selector: 'app-regiao-form',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, ButtonModule, CheckboxModule, DropdownModule, AutoCompleteModule, TableModule, ToastModule],
  providers: [MessageService],
  templateUrl: './regiao-form.component.html',
  styleUrl: './regiao-form.component.scss'
})
export class RegiaoFormComponent implements OnInit {
  regiao: Regiao = { id: "", nome: '', ativa: true, cidades: [] };
  editando = false;



  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private regiaoService: RegiaoService,
    private messageService: MessageService
  ) { }

  cidadeSelecionada: Cidade | null = null;
  cidadesFiltradas: Cidade[] = [];
  regiaoUfSelecionado: string = '';
  idSelecionado: string = '';

  buscarCidades(query: string) {
    if (query.length < 3) {
      this.cidadesFiltradas = [];
      return;
    }

    this.regiaoService.obterCidadesPorNome(query).subscribe(cidades => {
      const normalizar = (texto: string) =>
        texto.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase();

      const queryNormalizada = normalizar(query);

      this.cidadesFiltradas = cidades.filter(cidade =>
        normalizar(cidade.nome).includes(queryNormalizada)
      );
    });
  }


  aoSelecionarCidade(cidade: Cidade) {
    this.regiaoUfSelecionado = cidade.uf;
    this.idSelecionado = cidade.id;
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editando = true;
      const idNum = String(id);
      this.regiaoService.obterRegioes().subscribe(regioes => {
        const encontrada = regioes.find(r => r.id === idNum);
        if (encontrada) {
          this.regiao = { ...encontrada };
        } else {
          alert('Região não encontrada');
          this.router.navigate(['']);
        }
      });
    }
  }

  validarCidadeDigitada() {
    // Se já foi selecionada uma cidade válida, não faz nada
    if (this.cidadeSelecionada && this.cidadeSelecionada.id) {
      return;
    }

    const inputElement = document.querySelector('.input-cidade input') as HTMLInputElement;
    const inputValue = inputElement?.value?.trim();

    if (!inputValue) {
      this.limparCamposCidade();
      return;
    }

    const normalizar = (texto: string) =>
      texto
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '')
        .toLowerCase();

    const inputNormalizado = normalizar(inputValue);

    // Verifica se há uma correspondência exata (sem acento e case-insensitive)
    const cidadeExata = this.cidadesFiltradas.find(
      c => normalizar(c.nome) === inputNormalizado
    );

    if (cidadeExata) {
      this.cidadeSelecionada = cidadeExata;
      this.aoSelecionarCidade(cidadeExata);
    } else if (this.cidadesFiltradas.length > 0) {
      const cidade = this.cidadesFiltradas[0];
      this.cidadeSelecionada = cidade;
      this.aoSelecionarCidade(cidade);
    } else {
      this.limparCamposCidade();
    }
  }

  limparCamposCidade() {
    this.cidadeSelecionada = null;
    this.regiaoUfSelecionado = '';
    this.idSelecionado = '';
  }



  toastSucessoAtivo = false;

  salvar() {
    const cidadesIds = this.regiao.cidades.map(c => c.id);
    const request = {
      nome: this.regiao.nome,
      cidades: cidadesIds
    };

    this.regiaoService.inserirNovaRegiao(request).subscribe({
      next: (res) => {
        this.toastSucessoAtivo = true;
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: 'Região inserida com sucesso!',
          life: 1000
        });
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: err?.error?.message || 'Erro ao inserir região',
          life: 1000
        });
      }
    });
  }

  atualizar() {
    if (!this.editando) return; // Só atualiza se estiver no modo edição

    const cidadesIds = this.regiao.cidades.map(c => c.id);
    const dados = {
      id: this.regiao.id,
      nome: this.regiao.nome,
      Cidades: cidadesIds
    };

    this.regiaoService.atualizarRegiao(dados).subscribe({
      next: (res) => {
        this.toastSucessoAtivo = true;
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: 'Região atualizada com sucesso!',
          life: 1000
        });
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: err?.error?.message || 'Erro ao atualizar região',
          life: 2000
        });
      }
    });
  }



  aoFecharToast() {
    console.log('fechou')
    if (this.toastSucessoAtivo) {
      this.toastSucessoAtivo = false;
      this.router.navigate(['']);
    }
  }





  cancelar() {
    this.router.navigate(['']);
  }
  adicionarCidade() {
    if (this.cidadeSelecionada) {
      const jaExiste = this.regiao.cidades.some(c => c.id === this.cidadeSelecionada!.id);
      if (!jaExiste) {
        this.regiao.cidades = [...this.regiao.cidades, this.cidadeSelecionada];
      }

      // Limpa os campos após adicionar
      this.cidadeSelecionada = null;
      this.regiaoUfSelecionado = '';
      this.idSelecionado = '';
    }
  }


  removerCidade(cidade: Cidade) {
    this.regiao.cidades = this.regiao.cidades.filter(c => c.id !== cidade.id);
  }


}
