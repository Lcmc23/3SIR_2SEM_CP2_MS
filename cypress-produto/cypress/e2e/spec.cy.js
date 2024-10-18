describe('template spec', () => {
  it('passes', () => {
    cy.visit('http://127.0.0.1:5500/web-test/index.html')
    cy.get('#bemVindo').should('contain', 'Bem-vindo!')
  })
})