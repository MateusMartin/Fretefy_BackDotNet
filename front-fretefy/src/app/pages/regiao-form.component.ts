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



@Component({
  selector: 'app-regiao-form',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, ButtonModule, CheckboxModule, DropdownModule, AutoCompleteModule, TableModule],
  templateUrl: './regiao-form.component.html',
  styleUrl: './regiao-form.component.scss'
})
export class RegiaoFormComponent implements OnInit {
  regiao: Regiao = { id: "", nome: '', ativa: true, cidades: [] };
  editando = false;



  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private regiaoService: RegiaoService
  ) { }

  cidadeSelecionada: Cidade | null = null;
  cidadesFiltradas: Cidade[] = [];
  regiaoUfSelecionado: string = '';
  idSelecionado: string = '';

  buscarCidades(query: string) {
    if (query.length >= 3) {
      this.regiaoService.obterCidadesPorNome(query).subscribe(cidades => {
        this.cidadesFiltradas = cidades;
      });
    } else {
      this.cidadesFiltradas = [];
    }
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
          this.router.navigate(['/regioes']);
        }
      });
    }
  }

  salvar() {
    console.log(this.editando ? 'Atualizando...' : 'Criando...', this.regiao);
    // Implementar chamada ao back-end aqui para salvar ou atualizar
    this.router.navigate(['/regioes']);
  }

  cancelar() {
    this.router.navigate(['/regioes']);
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
