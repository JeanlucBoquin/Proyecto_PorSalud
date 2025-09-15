var ClientesIndex = (function () {
    var state = { page: 1, pageSize: 20, search: "" };

    function cargar() {
        $("#contenedorTabla").load("/Clientes/Lista", state);
    }

    function wire() {
        $("#btnBuscar").on("click", function () {
            state.search = $("#txtSearch").val();
            state.page = 1;
            cargar();
        });

        // delegación para links de paginado
        $("#contenedorTabla").on("click", ".link-page", function (e) {
            e.preventDefault();
            state.page = $(this).data("page");
            cargar();
        });
    }

    return {
        init: function () { wire(); cargar(); }
    };
})();
