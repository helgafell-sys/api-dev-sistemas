import java.util.List;

class SistemaRestaurante {
    public static void main(String[] args) {
        // Criando alguns itens do menu
        List<String> ingredientesLasanha = List.of("Massa", "Queijo", "Molho de tomate", "Carne moída");
        PratoPrincipal lasanha = new PratoPrincipal(
                "Lasanha Bolonhesa", 45.90f, "Lasanha tradicional com molho bolonhesa",
                ingredientesLasanha, 30, "Massas"
        );

        List<String> ingredientesSalada = List.of("Alface", "Tomate", "Cebola", "Azeitonas");
        PratoPrincipal salada = new PratoPrincipal(
                "Salada Caesar", 25.50f, "Salada clássica com molho Caesar",
                ingredientesSalada, 10, "Vegetariano"
        );

        Bebida refrigerante = new Bebida(
                "Refrigerante", 8.50f, "Refrigerante gelado",
                "500ml", false, "Coca-Cola"
        );

        Bebida cerveja = new Bebida(
                "Cerveja Artesanal", 15.90f, "Cerveja IPA artesanal",
                "350ml", true, "Colorado"
        );

        // Demonstração de alteração de preço (encapsulamento)
        System.out.println("Preço original da lasanha: R$" + lasanha.getPreco());
        lasanha.alterarPreco(48.50f);
        System.out.println("Novo preço da lasanha: R$" + lasanha.getPreco());

        // Criando pedidos
        Pedido pedido1 = new Pedido();
        pedido1.adicionarItem(lasanha);
        pedido1.adicionarItem(refrigerante);
        pedido1.exibirPedido();

        Pedido pedido2 = new Pedido();
        pedido2.adicionarItem(salada);
        pedido2.adicionarItem(cerveja);
        pedido2.adicionarItem(refrigerante);
        pedido2.exibirPedido();
    }
}