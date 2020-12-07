<template>
    <v-toolbar app>
        <v-toolbar-title class="headline text-upper mr-4">
            <span>Game Loan </span>
            <span class="font-weight-light">Manager</span>
        </v-toolbar-title>
        <v-toolbar-items>
            <v-btn flat to="/">Início</v-btn>
            <v-btn flat to="/user" v-if="isLoggedIn">Usuários</v-btn>
            <v-btn flat to="/game" v-if="isLoggedIn">Jogos</v-btn>
            <v-btn flat to="/loanGame" v-if="isLoggedIn">Jogos Emprestados</v-btn>
        </v-toolbar-items>
        
        <v-spacer></v-spacer>

        <v-toolbar-items>
            <v-menu offset-y v-if="isLoggedIn">
                <v-btn flat slot=activator>Bem vindo {{ loggedInUser }}</v-btn>
                <v-list>
                    <v-list-tile @click="logout">
                        <v-list-tile-title>Sair</v-list-tile-title>
                    </v-list-tile>
                </v-list>
            </v-menu>
            <template v-else>
                <v-btn flat to="/user/new">Registrar Usuário</v-btn>
                <v-btn flat to="/login">Login</v-btn>
            </template>
        </v-toolbar-items>
    </v-toolbar>
</template>

<script>

export default {
    computed: {
        isLoggedIn() {
            return this.$store.getters.isLoggedIn
        },
        loggedInUser() {
            return this.$store.getters.loggedInUser ? this.$store.getters.loggedInUser.user : '';
        },
        
    },
    methods: {
        logout(){
            return this.$store.dispatch("logout");
        } 
    }
}
</script>

<style>

</style>
