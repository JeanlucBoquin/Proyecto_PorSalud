var ClientesIndex = (function () {
    var state = { page: 1, pageSize: 20, search: "" };

    function cargar() {
        //$("#contenedorTabla").load("/Clientes/Lista", state);
        $("#contenedorTabla").load("/Clientes/Lista" + '?' + $.param({ page: state.page, pageSize: state.pageSize, search: state.search }));

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
            console.log(e)
            state.page = $(this).data("page");
            cargar();
        });

        // cambiar tamaño de página
        $(document).on("change.clientes", "#pageSize[data-page-size]", function () {
            var ps = parseInt($(this).val(), 10);
            if (!isNaN(ps) && ps > 0) {
                state.pageSize = ps;  // usa tu estado existente
                state.page = 1;       // reinicia a la primera página
                cargar();
            }
        });

    }

    return {
        init: function () { wire(); cargar(); }
    };
})();
