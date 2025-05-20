import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Cidade {
    id: string;
    nome: string;
    uf: string;
}

export interface Regiao {
    id: string;
    nome: string;
    ativa: boolean;
    cidades: Cidade[];
}

@Injectable({
    providedIn: 'root',
})
export class RegiaoService {
    private apiUrl = 'http://localhost:5000/api/';

    constructor(private http: HttpClient) { }

    obterRegioes(): Observable<Regiao[]> {
        const headers = { 'incluirInativas': 'true' };
        return this.http.get<Regiao[]>(this.apiUrl + 'regiao/get', { headers });
    }

    obterCidadesPorNome(nomeParcial: string): Observable<Cidade[]> {
        return this.http.get<Cidade[]>(this.apiUrl + 'cidade', {
            params: { terms: nomeParcial }
        });
    }

    inserirNovaRegiao(regioes: Regiao) {


    }


}
