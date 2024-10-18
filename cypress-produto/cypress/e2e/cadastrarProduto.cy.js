// Funções para gerar valores aleatórios
function gerarStringAleatoria(tamanho) {
  const caracteres = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let resultado = '';

  for (let i = 0; i < tamanho; i++) {
    const indice = Math.floor(Math.random() * caracteres.length);
    resultado += caracteres[indice];
  }

  return resultado;
}

function gerarPrecoAleatorio() {
  return (Math.random() * (500 - 10) + 10).toFixed(2);
}

function gerarQuantidadeAleatoria() {
  return Math.floor(Math.random() * 100) + 1;
}

function gerarDataAleatoria() {
  const hoje = new Date();
  const dia = String(hoje.getDate()).padStart(2, '0');
  const mes = String(hoje.getMonth() + 1).padStart(2, '0');
  const ano = hoje.getFullYear();
  return `${ano}-${mes}-${dia}`;
}

describe('Cadastro de Produtos', () => {
  it('deve cadastrar um produto com sucesso', () => {
    cy.visit('http://127.0.0.1:5500/web-test/cadastro.html');

    const nome = gerarStringAleatoria(10);
    const preco = gerarPrecoAleatorio();
    const quantidade = gerarQuantidadeAleatoria();
    const data = gerarDataAleatoria();

    cy.get('#registerName').type(nome);
    cy.get('#registerPreco').type(preco);
    cy.get('#registerQuantidade').type(quantidade);
    cy.get('#registerData').type(data);

    cy.get('.btn').click();

    cy.url().should('include', 'listar.html');
  });
});
