<template>
  <v-layout row wrap align-center justify-center>
    <v-flex class="pl-3 pr-3 pb-3" xs10 md6 lg4>
      <v-card flat class="pa-3">
        <v-card-title primary-title>
          <h4>Login</h4>
        </v-card-title>
        <v-form>
          <v-text-field
            prepend-icon="person"
            name="username"
            label="Usuário"
            v-model="auth.login"
          ></v-text-field>
          <v-text-field
            prepend-icon="lock"
            name="password"
            label="Senha"
            type="password"
            v-model="auth.password"
          ></v-text-field>
          <v-card-actions>
            <v-btn primary large block @click.prevent="login">Login</v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  data() {
    return {
      auth: {
        login: '',
        password: ''
      }
    };
  },
  created() {
      this.$store.dispatch("logout")
  },
  methods: {
    login() {
      this.$store.dispatch("authentication", this.auth)
                 .then(() => this.$router.push('/'));
    },
  },
};
</script>

<style>
</style>
