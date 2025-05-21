import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

export interface RegiaoInsertRequest {
    nome: string;
    cidades: string[]; // Lista de GUIDs (string)
}

export interface RegiaoPutRequest {
    id: string;
    nome: string;
    Cidades: string[];
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

    inserirNovaRegiao(request: RegiaoInsertRequest): Observable<any> {
        return this.http.post(this.apiUrl + 'regiao/insert', request);
    }

    atualizarRegiao(request: RegiaoPutRequest): Observable<any> {
        return this.http.put(this.apiUrl + 'regiao/put', request);
    }

    inativarRegiao(regiaoId: string): Observable<any> {
        const headers = new HttpHeaders().set('regiaoId', regiaoId);
        return this.http.delete(this.apiUrl + 'regiao/Delete', { headers });
    }


    removerRegiao(regiaoId: string): Observable<any> {
        const headers = new HttpHeaders().set('regiaoId', regiaoId);
        return this.http.delete(this.apiUrl + 'regiao/Remover', { headers });
    }



}
