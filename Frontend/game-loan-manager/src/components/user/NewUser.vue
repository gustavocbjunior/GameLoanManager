<template>
  <v-layout row wrap align-center justify-center>
    <v-alert v-model="alert" dismissible :type="alertType" class="alertMessage">
      {{ alertMessage }}
    </v-alert>
    <v-flex class="pl-3 pr-3 pb-3" xs12 md10 lg8>
      <v-card flat class="pa-3">
        <v-card-title primary-title>
          <h4>Novo Usuário</h4>
        </v-card-title>
        <v-form>
          <v-text-field
            prepend-icon="person"
            name="login"
            label="Login"
            v-model="user.login"
            :rules="[rules.required, rules.min]"
            counter
          ></v-text-field>
          <v-text-field
            prepend-icon="lock"
            name="password"
            label="Senha"
            :type="show1 ? 'text' : 'password'"
            v-model="user.password"
            :append-icon="show1 ? 'visibility' : 'visibility_off'"
            :rules="[rules.required, rules.minPassword]"
            counter
            hint="At least 8 characters"
            @click:append="show1 = !show1"
          ></v-text-field>
          <v-text-field
            prepend-icon="contacts"
            name="Name"
            label="Nome"
            v-model="user.name"
            clearable
          ></v-text-field>
          <v-text-field
            prepend-icon="mail"
            name="email"
            label="E-mail"
            v-model="user.email.address"
            :rules="[rules.required, rules.email]"
            clearable
          ></v-text-field>
          <v-text-field
            prepend-icon="phone"
            name="phone"
            label="Telefone"
            v-model="user.phone"
            clearable
            mask="(##) ##### - ####"
          ></v-text-field>
          <v-card-actions>
            <v-btn primary large block @click.prevent="register" color="success"
              >Registrar</v-btn
            >
            <v-btn primary large block @click="clear">Limpar</v-btn>
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
      user: {
        login: "",
        password: "",
        name: "",
        email: {
          address: "",
        },
        phone: "",
      },
      defaultUser: {
        login: "",
        password: "",
        name: "",
        email: {
          address: "",
        },
        phone: "",
      },
      rules: {
        required: (value) => !!value || "Obrigatório.",
        min: (v) => v.length >= 3 || "Min 3 caracteres",
        minPassword: (v) => v.length >= 8 || "Min 8 caracteres",
        email: (value) => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || "E-mail inválido.";
        },
      },
      show1: false,
      alert: false,
      alertType: "success",
      alertMessage: "",
    };
  },
  methods: {
    register() {
      this.$store
        .dispatch("userRegister", this.user)
        .then((response) => {
          console.log(response.message);
          var message = response.message;
          this.showAlert("success", message);

          this.clear();
        })
        .catch((err) => {
          if (err.status == 401) {
            this.$store.dispatch("logout");
            this.showAlert("error", "Acesso expirado, faça login novamente.");
          } else {
            console.log(err);
            this.showAlert("error", err.data.message);
          }
        });
    },
    clear() {
      this.user = Object.assign({}, this.defaultUser);
      //this.user.name = '';
    },
    showAlert(type, message) {
      this.alert = true;
      this.alertType = type;
      this.alertMessage = message;
    },
  },
};
</script>

<style>
</style>
